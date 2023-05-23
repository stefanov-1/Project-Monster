using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    //util function from chatGPT :)
    public static Vector3 ClosestPointOnLineSegment(Vector3 start, Vector3 end, Vector3 point)
    {
        // Calculate the vector representing the line segment
        Vector3 line = end - start;

        // Calculate the vector from the start point to the third point
        Vector3 pointStart = point - start;

        // Calculate the projection of pointStart onto the line segment
        float projection = Vector3.Dot(pointStart, line) / line.sqrMagnitude;

        // If the projection is less than zero, the closest point is the start point
        if (projection < 0)
        {
            return start;
        }

        // If the projection is greater than one, the closest point is the end point
        if (projection > 1)
        {
            return end;
        }

        // Otherwise, the closest point is the projection of the third point onto the line segment
        Vector3 closestPoint = start + projection * line;

        return closestPoint;
    }
}
