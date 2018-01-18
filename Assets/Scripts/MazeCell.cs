using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCell : MonoBehaviour {
    public intVector2 cellCoordinates;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirectionClass.counter];
    private int initializedEdgeCount;
    public bool isFullyInitialized
    {
        get { return initializedEdgeCount == MazeDirectionClass.counter; }
    }
    public MazeCellEdge getEdge(MazeDirection direction)
    {
        return edges[(int)direction];
    }

    public void setEdge (MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int)direction] = edge;
        initializedEdgeCount += 1;
    }
   
    public MazeDirection RandomUninitializedDirection
    {
        get { int skips = Random.Range(0, MazeDirectionClass.counter - initializedEdgeCount);
            for (int i = 0; i < MazeDirectionClass.counter; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection)i;
                    }
                    skips -= 1;
                }
            }
            throw new System.InvalidOperationException("No uninitialized cells");
        }
    }
}
