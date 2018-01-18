using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct intVector2{
    public int x, z;
    public intVector2 (int x ,int z)
    {
        this.x = x;
        this.z = z;
    }
    public static intVector2 operator + (intVector2 first, intVector2 second)
    {
        first.x += second.x;
        first.z += second.z;
        return first;
    }
}
