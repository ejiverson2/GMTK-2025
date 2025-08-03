using Godot;
using System;

public partial class PlaneSelection : Control
{
	private int currentPlane = 1;
	[Export] int totalPlanes = 2; // Total number of planes available
	[Export] string gamePath = ""; // Path to the plane scenes
	public override void _Ready()
	{
		GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/PreviousButton").Disabled = true;
	}
	private void _set_plane_visibility(string planePath, int planeNumber, int totalPlanes)
	{
		string fullPath;
		for (int i = 1; i <= totalPlanes; i++)
		{
			if (i == planeNumber)
			{
				fullPath = $"{planePath}/Plane{i}";
				GetNode<TextureRect>(fullPath).Visible = true;
				fullPath = $"{planePath}/Plane{i}_1";
				GetNode<TextureRect>(fullPath).Visible = true;
				fullPath = $"{planePath}/Plane{i}_2";
				GetNode<TextureRect>(fullPath).Visible = true;
			}
			else
			{
				fullPath = $"{planePath}/Plane{i}";
				GetNode<TextureRect>(fullPath).Visible = false;
				fullPath = $"{planePath}/Plane{i}_1";
				GetNode<TextureRect>(fullPath).Visible = false;
				fullPath = $"{planePath}/Plane{i}_2";
				GetNode<TextureRect>(fullPath).Visible = false;
			}
		}
	}
	private void _on_next_button_pressed()
	{
		currentPlane++;
		_set_plane_visibility("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer", currentPlane, totalPlanes);
		if (currentPlane >= totalPlanes)
		{
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/NextButton").Disabled = true;
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/PreviousButton").Disabled = false;
		}
		else
		{
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/NextButton").Disabled = false;
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/PreviousButton").Disabled = false;
		}
	}
	private void _on_previous_button_pressed()
	{
		currentPlane--;
		_set_plane_visibility("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer", currentPlane, totalPlanes);
		if (currentPlane <= 1)
		{
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/PreviousButton").Disabled = true;
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/NextButton").Disabled = false;
		}
		else
		{
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/PreviousButton").Disabled = false;
			GetNode<Button>("CanvasLayer/PanelContainer/MarginContainer/VBoxContainer/HBoxContainer/NextButton").Disabled = false;
		}
	}
	private void _on_play_button_pressed()
	{
		PackedScene gameScene = GD.Load<PackedScene>(gamePath);
		GetTree().ChangeSceneToFile(gamePath);
	}
}
