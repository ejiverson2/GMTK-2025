using System.IO;
using Godot;

public class TrailSegment {

    public Vector2 Beginning;
    public Vector2 End;
    Trail trail;
    Area2D area;

    public TrailSegment(Vector2 beginning, Vector2 end, Trail trail) {
        Beginning = beginning;
        End = end;
        this.trail = trail;

        // For line physics collisions

        // SegmentShape2D shape = new() {
        //     A = beginning,
        //     B = end
        // };

        // CollisionShape2D collisionShape = new() {
        //     Shape = shape
        // };


        // area = new Area2D();
        // area.AddChild(collisionShape);
        // area.Monitoring = true;
        // area.Monitorable = false;
        // area.CollisionLayer = 0;
        // area.CollisionMask = 1 << 7; //only layer 8 is active
        // area.AreaEntered += OnAreaEntered;

        // trail.Parent.GetTree().Root.AddChild(area);
    }

    public void Destroy() {
        area?.QueueFree();
    }

    // Collision Event
    void OnAreaEntered(Area2D intersectingArea) {
    }
}