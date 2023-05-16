using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if(player.isGrounded)
        {
            return player.idleState;
        }
        if(!player.isGrounded)
        {
            return player.inAirState;
        }
        return player.jumpingState;
    }

    public override void EnterState(PlayerStateManager player)
    {
        player.rb.AddForce(new Vector3(0, player.jumpForce, 0), ForceMode.Impulse);
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
}
