using Godot;
using System;

public partial class FallingCat : Loopable {
	[Export] float fallingSpeed = 100f; // in px per second
	[Export] float despawnFloor = 1300f;

	public override void _Process(double delta) {
		Position += new Vector2(0, fallingSpeed) * (float)delta;

		if (Position.Y > despawnFloor) {
			QueueFree();
		}
	}

	public override void Looped() {
		QueueFree();
	}
}
