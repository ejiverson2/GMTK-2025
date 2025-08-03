using Godot;
using System;

public partial class GameOverMenu : Control
{
	private void _on_replay_pressed()
	{
		PackedScene gameScene = GD.Load<PackedScene>("res://Scenes/level_0.tscn");
		GetTree().ChangeSceneToFile("res://Scenes/level_0.tscn");
	}
	private void _on_change_plane_pressed()
	{
		PackedScene planeSelectionScene = GD.Load<PackedScene>("res://Scenes/ui/plane_selection.tscn");
		GetTree().ChangeSceneToFile("res://Scenes/ui/plane_selection.tscn");
	}
	private void _on_menu_pressed()
	{
		PackedScene menuScene = GD.Load<PackedScene>("res://Scenes/ui/Menu.tscn");
		GetTree().ChangeSceneToFile("res://Scenes/ui/Menu.tscn");
	}
	
}
