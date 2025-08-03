using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node2D {
    [ExportCategory("Dependencies")]
    [Export] Camera camera;
    [Export] Player player;
    [Export] Trail trail;

    [ExportCategory("Spawnables")]
    [Export] PackedScene fallingCatScene;

    [ExportCategory("Settings")]
    [Export] float trailSegmentLength = 30f; //in px

    RandomNumberGenerator rng;
    int score = 0;

    public override void _Ready() {
        trail.Initialize(player.Position);
        trail.LoopCreated += OnTrailLoopCreated;

        rng = new RandomNumberGenerator();
        CreateCatSpawner();
    }

    public override void _Process(double delta) {
        // Do trail
        if (player.Position.DistanceTo(trail.GetLeadingPoint()) >= trailSegmentLength) {
            trail.AddSegment(player.Position);
        }
        trail.Render();
    }

    // Looping Mechanic

    void CreateLoopVisual(Vector2[] points) {
        Line2D loop = new Line2D();
        loop.Points = points;
        loop.Closed = true;
        loop.DefaultColor = Colors.Yellow;

        GetTree().Root.AddChild(loop);
    }

    void ActivateLoopables(Vector2[] loopPoints) {
        Rid shapeRid = PhysicsServer2D.ConvexPolygonShapeCreate();
        PhysicsServer2D.ShapeSetData(shapeRid, loopPoints);

        PhysicsShapeQueryParameters2D query = new PhysicsShapeQueryParameters2D();
        query.ShapeRid = shapeRid;
        query.CollideWithAreas = true;
        query.CollideWithBodies = false;
        query.CollisionMask = 1 << 2;

        PhysicsDirectSpaceState2D spaceState = GetWorld2D().DirectSpaceState;
        Godot.Collections.Array<Godot.Collections.Dictionary> loopedObjects = spaceState.IntersectShape(query);
        GD.Print(loopedObjects.Count);
        foreach (Godot.Collections.Dictionary loopedObject in loopedObjects) {
            Node2D collider = (Area2D)loopedObject["collider"];
            if (collider.GetParent() is Loopable loopable) {
                loopable.Looped();
            }
        }

        // CollisionPolygon2D loopShape = new CollisionPolygon2D {
        // 	Polygon = loopPoints
        // };

        // Area2D loopArea = new Area2D {
        // 	Monitoring = true,
        // 	Monitorable = false,
        // 	CollisionLayer = 0,
        // 	CollisionMask = 1 << 3
        // };
        // loopArea.AddChild(loopShape);

        // GetTree().Root.AddChild(loopArea);

        // Godot.Collections.Array<Area2D> loopedAreas = loopArea.GetOverlappingAreas();
        // GD.Print(loopedAreas.Count);
        // foreach (Area2D loopedArea in loopedAreas) {
        // 	if (loopedArea.GetParent() is Loopable loopable) {
        // 		loopable.Looped();
        // 	}
        // }

        // loopArea.QueueFree();

        // Loopable[] loopablesArray = loopables.ToArray();
        // foreach (Loopable loopable in loopablesArray) {
        // 	if (Geometry2D.IsPointInPolygon(loopable.Position, loopPoints)) {
        // 		loopables.Remove(loopable);
        // 		loopable.Looped();
        // 		GD.Print("cat looped");
        // 	}
        // }
    }

    //Event Handlers
    void OnTrailLoopCreated(Vector2[] loopPoints) {
        CreateLoopVisual(loopPoints);
        ActivateLoopables(loopPoints);
    }

    //Cats

    void CreateCatSpawner() {
        Timer spawnTimer = new() {
            WaitTime = .5,
            Autostart = true
        };
        spawnTimer.Timeout += SpawnRandomCat;
        AddChild(spawnTimer);
    }

    void SpawnRandomCat() {
        FallingCat newCat = fallingCatScene.Instantiate<FallingCat>();
        newCat.Position = new Vector2(player.Position.X + (rng.Randf() - 0.5f) * 500, -50);

        GetTree().Root.AddChild(newCat);
    }
}
