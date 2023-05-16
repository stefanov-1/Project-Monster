using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return player.jumpingState;
        }

        float input = Input.GetAxis("Horizontal");
        Vector3 climbDirection = ControlValues.Instance.currentClimbEnd - ControlValues.Instance.currentClimbStart;
        climbDirection.Normalize();

        Vector3 newPosition = player.rb.position + climbDirection * input * player.climbSpeed * Time.deltaTime;
        if (Vector3.Distance(newPosition, ControlValues.Instance.currentClimbStart) > 1.5f
            && Vector3.Distance(newPosition, ControlValues.Instance.currentClimbEnd) > 1.5f)
            player.rb.MovePosition(newPosition);
        
        return player.climbingState;
    }
    
    public override void EnterState(PlayerStateManager player)
    {
        player.rb.useGravity = false;
        player.rb.velocity = Vector3.zero;

        Vector3 closetsPoint = ClosestPointOnLineSegment(
            ControlValues.Instance.currentClimbStart,
            ControlValues.Instance.currentClimbEnd,
            player.rb.position);

        player.rb.position = closetsPoint; // snap the player to the clmbable surface

    }

    public override void ExitState(PlayerStateManager player)
    {
        player.rb.useGravity = true;
    }
    
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
