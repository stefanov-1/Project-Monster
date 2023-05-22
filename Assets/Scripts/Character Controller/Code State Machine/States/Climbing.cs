using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : State
{
    public override void UpdateState(PlayerStateManager player)
    {
        player.rb.velocity = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(player.jumpingState);
            return;
        }

        float input = ControlValues.Instance.currentClimbOrientation == ControlValues.ClimbOrientation.LeftRight 
            ? Input.GetAxis("Horizontal") : Input.GetAxis("Vertical");

        Vector3 climbDirection = ControlValues.Instance.currentClimbEnd - ControlValues.Instance.currentClimbStart;
        climbDirection.Normalize();

        Vector3 closer = Vector3.Distance(player.rb.position, ControlValues.Instance.currentClimbStart) <
                         Vector3.Distance(player.rb.position, ControlValues.Instance.currentClimbEnd)
            ? ControlValues.Instance.currentClimbStart
            : ControlValues.Instance.currentClimbEnd;

        //these two ifs are here to prevent the player from climbing outside of the climb area
        if (closer == ControlValues.Instance.currentClimbStart &&
            Vector3.Distance(player.rb.position, closer) < 0.2f &&
            input < 0)
            return;
        
        if (closer == ControlValues.Instance.currentClimbEnd &&
            Vector3.Distance(player.rb.position, closer) < 0.2f &&
            input > 0)
            return;

        player.rb.velocity = climbDirection * input * player.climbSpeed;
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }
    
    public override void EnterState(PlayerStateManager player)
    {
        player.rb.velocity = Vector3.zero;

        Vector3 closetsPoint = ClosestPointOnLineSegment(
            ControlValues.Instance.currentClimbStart,
            ControlValues.Instance.currentClimbEnd,
            player.rb.position);

        player.rb.position = closetsPoint; // snap the player to the clmbable surface
        Debug.Log($"taraget: {closetsPoint}");
        Debug.Log($"actual: {player.rb.position}");
    }

    public override void ExitState(PlayerStateManager player)
    {
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
