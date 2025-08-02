using Godot;
using System;

public partial class Player : CharacterBody2D {
	// Dependencies
	[Export] Line2D trailVFX;
	[Export] Node2D trailEmissionPoint;
	[Export] Area2D TrailCollider;
	// Settings
	float rotationSpeed = 180f; // in deg/sec
								// float rotationSpeed = 360f; // in deg/sec
	float initialSpeed = 500f; //in px/s
	float gravity = 2000f; //in px/s^2
	float thrust = 50f; //in px/s^2
	float minSpeed = 30;
	float currentSpeed;
	int trailMaxPoints = 500;
	float trailSegmentLength = 30; // in px


	// Internal Variables
	Trail trail;

	public override void _Ready() {
		trail = new Trail(this, trailMaxPoints);
		trail.AddSegment(Position);

		Velocity = new Vector2(initialSpeed, 0);
		currentSpeed = initialSpeed;
	}


	public override void _PhysicsProcess(double delta) {
		// apply gravity
		// Velocity = Transform.X * airSpeed;
		ApplyPhysicsWithGravity((float)delta);

		if (Input.IsActionPressed("rotate_clockwise")) {
			RotationDegrees += rotationSpeed * (float)delta;
		} else if (Input.IsActionPressed("rotate_counterclockwise")) {
			RotationDegrees -= rotationSpeed * (float)delta;
		}

		MoveAndSlide();

		// Do trail
		if (trailEmissionPoint.GlobalPosition.DistanceTo(trail.GetLeadingPoint()) >= trailSegmentLength) {
			trail.AddSegment(trailEmissionPoint.GlobalPosition);
		}

		trailVFX.Points = trail.GetPointsArray();
	}

	void ApplyPhysicsWithGravity(float delta) {
		// // forward vector
		// currentSpeed += Transform.X.Dot(Vector2.Down) * gravity * delta;
		if (Position.Y < 20) {
			LookAt(Position + Transform.X.Reflect(Vector2.Right));
		}

		currentSpeed = Mathf.Sqrt(Mathf.Max(0, Position.Y - 20)) * 50;
		if (currentSpeed <= minSpeed) {
			currentSpeed = minSpeed;
		}

		Velocity = Transform.X * currentSpeed;



		GD.Print(Velocity);
	}
}
