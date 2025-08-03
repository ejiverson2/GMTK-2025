using Godot;
using System;

public partial class RebindControlButton : GridContainer
{
    private bool listeningForKey = false;
    private Button currentButton = null;
    private OptionsMenu optionsMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get the root node of the current scene
		optionsMenu = GetTree().CurrentScene as OptionsMenu;
		Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
        GetNode<Button>("Button").Text = gameSettings.pitchUpKey.ToString();
		GetNode<Button>("Button2").Text = gameSettings.pitchDownKey.ToString();
		
	}
    

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Input(InputEvent @event)
    {
        if (listeningForKey && @event is InputEventKey keyEvent && keyEvent.IsPressed() && !keyEvent.IsEcho())
        {
            if (currentButton != null)
            {
                currentButton.Text = $"{keyEvent.Keycode}";
                currentButton.Disabled = false;
            }
            listeningForKey = false;
            currentButton = null;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (listeningForKey && @event is InputEventKey keyEvent && keyEvent.IsPressed() && !keyEvent.IsEcho())
        {
            if (currentButton != null)
            {
                currentButton.Text = $"{keyEvent.Keycode}";
                currentButton.Disabled = false;

                // Set the key in OptionsMenu
                if (currentButton.Name == "Button" && optionsMenu != null)
                    optionsMenu.pitchUpKey = keyEvent.Keycode;
                else if (currentButton.Name == "Button2" && optionsMenu != null)
                    optionsMenu.pitchDownKey = keyEvent.Keycode;
            }
            listeningForKey = false;
            currentButton = null;
            GetViewport().SetInputAsHandled(); // Prevents the key from being used elsewhere
        }
    }

    private void _on_button_pressed()
    {
        listeningForKey = true;
        currentButton = GetNode<Button>("Button");
        currentButton.Text = "Press a key...";
        currentButton.Disabled = true;
    }

    private void _on_button_2_pressed()
    {
        listeningForKey = true;
        currentButton = GetNode<Button>("Button2");
        currentButton.Text = "Press a key...";
        currentButton.Disabled = true;
    }
}
