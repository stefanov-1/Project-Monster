using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Idle : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            return player.runningState;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return player.jumpingState;
        }
        if(!player.isGrounded)
        {
            return player.inAirState;
        }   
        return player.idleState;
    }

    public override void EnterState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
}
