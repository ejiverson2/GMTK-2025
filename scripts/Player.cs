using Godot;
using System;

public partial class Player : CharacterBody2D {
    // Settings
    float rotationSpeed = 180f; // in deg/sec
    float airSpeed = 500f;
    float gravity = 9.8f; //in m/s
    int trailMaxPoints = 30;
    float trailSegmentLength = 30; // in px

    // Internal Variables
    Trail trail;


    public override void _Ready() {
        // trail = new Trail(trailMaxPoints);
        // Velocity = new Vector2(100 * scale, 0);
    }


    public override void _PhysicsProcess(double delta) {
        // apply gravity
        // Velocity += new Vector2(0, gravity * (float)delta * scale);
        Velocity = Transform.X * airSpeed;

        if (Input.IsActionPressed("rotate_clockwise")) {
            RotationDegrees += rotationSpeed * (float)delta;
        } else if (Input.IsActionPressed("rotate_counterclockwise")) {
            RotationDegrees -= rotationSpeed * (float)delta;
        }

        MoveAndSlide();

        // Do trail
        // if (Position.DistanceTo(trail.GetLeadingPoint()) >= trailSegmentLength) {
        //     trail.Append(Position);
        // }
    }

}
