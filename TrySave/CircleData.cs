using System.Collections.Generic;
using UnityEngine;
public class CircleData:MonoBehaviour
{
    public Vector3 position;
    public float size;
    public Color color;

    public CircleData(Vector3 pos, float s, Color c)
    {
        position = pos;
        size = s;
        color = c;
    }
}