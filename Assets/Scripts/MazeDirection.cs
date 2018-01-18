using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MazeDirection { 
    North, East, South, West
}

public static class MazeDirectionClass
{
    public const int counter = 4;
    public static MazeDirection RandomValue
    {
        get { return (MazeDirection)Random.Range(0, counter); }
    }

    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    private static intVector2[] vectors =
    {
        new intVector2(0,1),
        new intVector2(1,0),
        new intVector2(0,-1),
        new intVector2(-1,0)
    };
    public static intVector2 ToIntVector2(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }


    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f,90f,0f),
        Quaternion.Euler(0f,180f,0f),
        Quaternion.Euler(0f,270f,0f)
    };


}
