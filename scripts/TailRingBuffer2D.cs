using Godot;
using System;

internal class TailRingBuffer2D
{
    private readonly int tailRingBufferCapacity; // = 6; //4096 * 128;
    private readonly Vector2[] tailRingBuffer; // = new Vector2[tailRingBufferCapacity];
    private int tailRingBufferInsertIndex = 0;
    private bool tailRingBufferFull = false;
    private Curve2D tailCurve2D = new Curve2D();

    public TailRingBuffer2D(int  tailRingBufferCapacity)
    {
        this.tailRingBufferCapacity = tailRingBufferCapacity;
        this.tailRingBuffer = new Vector2[tailRingBufferCapacity];
    }

    public int getTailRingBufferCapacity()
    {
        return tailRingBufferCapacity;
    }

    public int getTailRingBufferSize()
    {
        if (tailRingBufferFull) {
            return tailRingBufferCapacity;
        }
        return tailRingBufferInsertIndex;
    }

    private bool SegmentsIntersect(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
    {
        float d = (l1p2 - l1p1).Cross(l2p2 - l2p1);
        if (Mathf.IsZeroApprox(d))
            return false; // Parallel lines
        float t = (l2p1 - l1p1).Cross(l2p2 - l2p1) / d;
        float u = (l2p1 - l1p1).Cross(l1p2 - l1p1) / d;
        return t >= 0 && t <= 1 && u >= 0 && u <= 1;
    }

    private int incrementTailRingBufferIndex(int index)
    {
        int ret = ++index;
        if (ret >= tailRingBufferCapacity) {
            ret = 0;
        }
        return ret;
    }

    private int incrementTailRingBufferIndex(int index, int lastIndex)
    {
        if (index == lastIndex) {
            return index;
        }
        return incrementTailRingBufferIndex(index);
    }

    private int decrementTailRingBufferIndex(int index)
    {
        if (tailRingBufferFull) {
            if (index == 0) {
                return tailRingBufferCapacity - 1;
            }
            return --index;
        }
        if (index == 0) {
            return 0;
        }
        return --index;
    }

    private int decrementTailRingBufferIndex(int index, int firstIndex)
    {
        if (index == firstIndex) {
            return index;
        }
        return decrementTailRingBufferIndex(index);
    }

    public void insertTailRingBuffer(Vector2 pos)
    {
        if (tailRingBufferFull) {
            tailCurve2D.RemovePoint(0);
        }
        tailCurve2D.AddPoint(pos);
        tailRingBuffer[tailRingBufferInsertIndex] = pos;
        tailRingBufferInsertIndex = incrementTailRingBufferIndex(tailRingBufferInsertIndex);
        if (tailRingBufferInsertIndex == 0) {
            tailRingBufferFull = true;
        }
    }

    private int getTailRingBufferStartIndex()
    {
        if (tailRingBufferFull) {
            return tailRingBufferInsertIndex;
        }
        return 0;
    }

    private int getTailRingBufferLastIndex()
    {
        if (!tailRingBufferFull) {
            if (tailRingBufferInsertIndex == 0) {
                return 0;
            }
            return tailRingBufferInsertIndex - 1;
        }
        if (tailRingBufferInsertIndex == 0) {
            return tailRingBufferCapacity - 1;
        }
        return tailRingBufferInsertIndex - 1;
    }

    public Curve2D getTailCurve2D()
    {
        return tailCurve2D;
    }

    public Path2D getTailPath2D()
    {
        Path2D ret = new Path2D();
        ret.Curve = getTailCurve2D();
        return ret;
    }

    public Curve2D findTailRingBufferLoop()
    {
        // Brute-force check for segment intersections
        int tailRingBufferSize = getTailRingBufferSize();
        if (tailRingBufferSize < 4) {
            return null;
        }
        int tailRingBufferStartIndex = getTailRingBufferStartIndex();
        int tailRingBufferLastIndex = getTailRingBufferLastIndex();
        bool loopFound = false;
        int loopStartIndex = 0;
        int loopEndIndex = 0;
        int outerLoopLastIndex = decrementTailRingBufferIndex(tailRingBufferLastIndex, tailRingBufferStartIndex);
        for (int i = tailRingBufferStartIndex; i != outerLoopLastIndex; i = incrementTailRingBufferIndex(i)) {
            Vector2 line1p1 = tailRingBuffer[i];
            Vector2 line1p2 = tailRingBuffer[incrementTailRingBufferIndex(i)];
            for (int j = incrementTailRingBufferIndex(incrementTailRingBufferIndex(i, tailRingBufferLastIndex), tailRingBufferLastIndex); j != tailRingBufferLastIndex; j = incrementTailRingBufferIndex(j, tailRingBufferLastIndex)) {
                Vector2 line2p1 = tailRingBuffer[j];
                Vector2 line2p2 = tailRingBuffer[incrementTailRingBufferIndex(j)];
                if (SegmentsIntersect(line1p1, line1p2, line2p1, line2p2)) {
                    loopFound = true;
                    loopStartIndex = i;
                    loopEndIndex = incrementTailRingBufferIndex(j);
                    break;
                }
            }
            if (loopFound) {
                break;
            }
        }
        if (!loopFound) {
            return null;
        }
        Curve2D ret = new Curve2D();
        int idx = loopStartIndex;
        while (idx != loopEndIndex) {
            ret.AddPoint(tailRingBuffer[idx]);
            idx = incrementTailRingBufferIndex(idx);
        }
        ret.AddPoint(tailRingBuffer[loopEndIndex]);
        return ret;
    }

    public Curve2D findTailRingBufferLoopOpt()
    {
        // TODO: Add loop detection using spatial grid
        return findTailRingBufferLoop();
    }

}
