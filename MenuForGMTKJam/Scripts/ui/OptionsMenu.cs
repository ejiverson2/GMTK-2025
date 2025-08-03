using Godot;
using System;

public partial class OptionsMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	float MasterVolume = -6.0f;
	float MusicVolume = -6.0f;
	float SFXVolume = -6.0f;
	bool MasterVolumeMuted = false;
	bool MusicVolumeMuted = false;
	bool SFXVolumeMuted = false;
	public Key pitchUpKey = Key.W;
	public Key pitchDownKey = Key.S;
	
	public override void _Ready()
	{
		
		Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		if (gameSettings == null)
		{
			//GD.printErr("Failed to load Settings.tres");
			return;
		}
		MasterVolume = gameSettings.MasterVolume;
		MusicVolume = gameSettings.MusicVolume;
		SFXVolume = gameSettings.SFXVolume;
		MasterVolumeMuted = gameSettings.MasterVolumeMuted;
		MusicVolumeMuted = gameSettings.MusicVolumeMuted;
		SFXVolumeMuted = gameSettings.SFXVolumeMuted;
		pitchUpKey = gameSettings.pitchUpKey;
		pitchDownKey = gameSettings.pitchDownKey;


		

		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MasterVHSlider").Value = MasterVolume;
		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MusicVHSlider").Value = MusicVolume;
		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/SfxVHSlider").Value = SFXVolume;
		GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MuteMaster").ButtonPressed = MasterVolumeMuted;
		GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MuteMusic").ButtonPressed = MusicVolumeMuted;
		GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/MuteSfx").ButtonPressed = SFXVolumeMuted;

	}


	private void _on_master_vh_slider_value_changed(float value)
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		float MasterVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MasterVHSlider").Value;
		//GD.print("Master Volume set to: ", MasterVolume);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), MasterVolume);
		//gameSettings.MasterVolume = MasterVolume;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");
	}
	private void _on_music_vh_slider_value_changed(float value)
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		float MusicVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MusicVHSlider").Value;
		//GD.print("Music Volume set to: ", MusicVolume);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), MusicVolume);
		//gameSettings.MusicVolume = MusicVolume;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");
	}

	private void _on_sfx_vh_slider_value_changed(float value)
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		float SFXVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/SfxVHSlider").Value;
		//GD.print("SFX Volume set to: ", SFXVolume);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sfx"), SFXVolume);
		//gameSettings.SFXVolume = SFXVolume;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");
	}

	public void _on_reset_master_v_button_pressed()
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		//gameSettings.MasterVolume = -6.0f;
		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MasterVHSlider").Value = -6.0f;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");

		//GD.print("Master Volume reset to default: ", MasterVolume);
	}
	public void _on_reset_music_v_button_pressed()
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		//gameSettings.MusicVolume = -6.0f;
		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MusicVHSlider").Value = -6.0f;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");

		//GD.print("Music Volume reset to default: ", MusicVolume);
	}

	public void _on_reset_sfx_v_button_pressed()
	{
		//Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		//gameSettings.SFXVolume = -6.0f;
		GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/SfxVHSlider").Value = -6.0f;
		//ResourceSaver.Save(gameSettings, "res://Settings.tres");

		//GD.print("SFX Volume reset to default: ", SFXVolume);
	}
	public void _on_mute_master_toggled(bool button_pressed)
	{

		MasterVolumeMuted = button_pressed;
		//GD.print("Master Volume Muted: ", MasterVolumeMuted);
		
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), MasterVolumeMuted);

	}

	public void _on_mute_music_toggled(bool button_pressed)
	{

		MusicVolumeMuted = button_pressed;
		//GD.print("Music Volume Muted: ", MusicVolumeMuted);
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), MusicVolumeMuted);

	}

	public void _on_mute_sfx_toggled(bool button_pressed)
	{

		SFXVolumeMuted = button_pressed;
		//GD.print("SFX Volume Muted: ", SFXVolumeMuted);
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Sfx"), SFXVolumeMuted);

	}
	public void _on_save_pressed()
	{
		Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		gameSettings.MasterVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MasterVHSlider").Value;
		gameSettings.MusicVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MusicVHSlider").Value;
		gameSettings.SFXVolume = (float)GetNode<HSlider>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/SfxVHSlider").Value;
		gameSettings.MasterVolumeMuted = (bool)GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer/MuteMaster").ButtonPressed;
		gameSettings.MusicVolumeMuted = (bool)GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer2/MuteMusic").ButtonPressed;
		gameSettings.SFXVolumeMuted = (bool)GetNode<Button>("CanvasLayer/TabContainer/Audio/MarginContainer/VBoxContainer/HBoxContainer3/MuteSfx").ButtonPressed;
		string pitchUpKeyText = GetNode<Button>("CanvasLayer/TabContainer/Controls/MarginContainer/VBoxContainer/GridContainer/Button").Text;
		if (Enum.TryParse<Key>(pitchUpKeyText, out var parsedKey))
		{
			gameSettings.pitchUpKey = parsedKey;
			pitchUpKey = parsedKey; // Update the local variable as well
			InputMap.ActionEraseEvents("rotate_clockwise");
			InputMap.ActionAddEvent("rotate_clockwise", new InputEventKey { Keycode = parsedKey });
			InputMap.ActionAddEvent("rotate_clockwise", new InputEventMouseButton { ButtonIndex = MouseButton.Left, Pressed = true });
		}
		else
		{
			//GD.printErr($"Invalid key: {pitchUpKeyText}");
		}
		string pitchDownKeyText = GetNode<Button>("CanvasLayer/TabContainer/Controls/MarginContainer/VBoxContainer/GridContainer/Button2").Text;
		if (Enum.TryParse<Key>(pitchDownKeyText, out var parsedKey2))
		{
			gameSettings.pitchDownKey = parsedKey2;
			pitchDownKey = parsedKey2; // Update the local variable as well
			InputMap.ActionEraseEvents("rotate_counterclockwise");
			InputMap.ActionAddEvent("rotate_counterclockwise", new InputEventKey { Keycode = parsedKey });
			InputMap.ActionAddEvent("rotate_counterclockwise", new InputEventMouseButton { ButtonIndex = MouseButton.Right, Pressed = true });
		}
		else
		{
			//GD.printErr($"Invalid key: {pitchDownKeyText}");
		}
		

		

		ResourceSaver.Save(gameSettings, "res://Settings.tres");

	}
	public void _on_exit_pressed()
	{
		Settings gameSettings = GD.Load<Settings>("res://Settings.tres");
		MasterVolume = gameSettings.MasterVolume;
		MusicVolume = gameSettings.MusicVolume;
		SFXVolume = gameSettings.SFXVolume;
		MasterVolumeMuted = gameSettings.MasterVolumeMuted;
		MusicVolumeMuted = gameSettings.MusicVolumeMuted;
		SFXVolumeMuted = gameSettings.SFXVolumeMuted;
		pitchUpKey = gameSettings.pitchUpKey;
		pitchDownKey = gameSettings.pitchDownKey;

		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Master"), MasterVolume);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Music"), MusicVolume);
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex("Sfx"), SFXVolume);
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Master"), MasterVolumeMuted);
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Music"), MusicVolumeMuted);
		AudioServer.SetBusMute(AudioServer.GetBusIndex("Sfx"), SFXVolumeMuted);
		GetTree().ChangeSceneToFile("res://Scenes/ui/Menu.tscn");
	}


}
