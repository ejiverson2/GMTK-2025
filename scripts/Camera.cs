using Godot;
using System;

public partial class Camera : Camera2D {
    [Export] Node2D target;
    [Export] int xOffset = 0;
    [Export] int yPosition = 540;

    public override void _Process(double delta) {
        Position = new Vector2(target.Position.X + xOffset, yPosition);
        // Position = new Vector2(target.Position.X + xOffset, target.Position.Y);

    }

}
