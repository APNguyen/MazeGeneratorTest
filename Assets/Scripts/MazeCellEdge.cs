using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MazeCellEdge : MonoBehaviour {
    public MazeCell cell, connectingCell;
    public MazeDirection direction;


    public void initialization(MazeCell cell, MazeCell connectingCell, MazeDirection direction)
    {
        this.cell = cell;
        this.connectingCell = connectingCell;
        this.direction = direction;
        cell.setEdge(direction, this);
        transform.parent = cell.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = direction.ToRotation();
    }
}
