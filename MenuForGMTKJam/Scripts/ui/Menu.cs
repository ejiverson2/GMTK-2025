using Godot;
using System;

public partial class Menu : Control
{
	private OptionsMenu optionsMenuInstance;
	[Export] string plane_selection = "res://Scenes/ui/plane_selection.tscn"; // Path to the game scene
	private void _on_play_button_pressed()
	{
		PackedScene gameScene = GD.Load<PackedScene>(plane_selection);
		GetTree().ChangeSceneToFile(plane_selection);
	}
	public void _on_button_3_pressed()
	{
		PackedScene optionsMenuScene = GD.Load<PackedScene>("res://Scenes/ui/OptionsMenu.tscn");
		optionsMenuInstance = optionsMenuScene.Instantiate<OptionsMenu>();
		AddChild(optionsMenuInstance); // Adds OptionsMenu as a child of Menu
		CanvasLayer canvasLayer = GetNode<CanvasLayer>("CanvasLayer");
		canvasLayer.Visible = false; // Hides the Menu
	}
	
}
