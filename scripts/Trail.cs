using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Trail : Line2D {
	[Export] int maxSegments = 200;
	LinkedList<TrailSegment> segments;

	//Events
	[Signal] public delegate void LoopCreatedEventHandler(Vector2[] points);

	public void Initialize(Vector2 startPosition) {
		segments = new LinkedList<TrailSegment>();
		AddSegment(startPosition);
	}

	public void AddSegment(Vector2 point) {
		TrailSegment segment;

		// if no segments in trail, add segment with same start and end
		if (segments.Last == null) {
			segment = new TrailSegment(point, point, this);
		} else {
			segment = new TrailSegment(segments.Last.Value.End, point, this);
		}

		int segmentsLeftToCount = segments.Count;
		for (LinkedListNode<TrailSegment> node = segments.First; node != segments.Last; node = node.Next) {
			Variant intersectionPoint = Geometry2D.SegmentIntersectsSegment(node.Value.Beginning, node.Value.End, segment.Beginning, segment.End);

			if (intersectionPoint.VariantType != Variant.Type.Nil) { // a loop has been detected!
				GD.Print("intersectionPoint = " + (Vector2)intersectionPoint);

				Vector2[] loopPoints = GetLoopPoints(segmentsLeftToCount, node, (Vector2)intersectionPoint);
				EmitSignal(SignalName.LoopCreated, loopPoints);

				segments.Clear();
				break;
			}

			segmentsLeftToCount--;
		}

		segments.AddLast(new LinkedListNode<TrailSegment>(segment));

		if (segments.Count > maxSegments) {
			RemoveEndSegment();
		}
	}

	Vector2[] GetLoopPoints(int numSegments, LinkedListNode<TrailSegment> startSegment, Vector2 intersectionPoint) {
		Vector2[] points = new Vector2[numSegments + 1];
		points[0] = intersectionPoint;
		int i = 1;

		for (LinkedListNode<TrailSegment> node = startSegment; node != null; node = node.Next) {
			points[i] = node.Value.End;
			i++;
		}

		return points;
	}

	void RemoveEndSegment() {
		segments.First.Value.Destroy();
		segments.RemoveFirst();
	}

	public Vector2 GetLeadingPoint() {
		return segments.Last.Value.End;
	}

	public Vector2[] GetPointsArray() {
		Vector2[] points = new Vector2[segments.Count + 1];

		points[0] = segments.Last.Value.End;
		int i = 1;

		for (LinkedListNode<TrailSegment> node = segments.Last; node != null; node = node.Previous) {
			points[i] = node.Value.Beginning;
			i++;
		}

		return points;
	}

	public void Render() {
		Points = GetPointsArray();
	}
}
