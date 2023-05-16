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
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.rb.useGravity = true;
    }
    
}
