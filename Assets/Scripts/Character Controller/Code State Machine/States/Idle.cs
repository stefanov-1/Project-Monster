using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Idle : State
{
    public override void UpdateState(PlayerStateManager player)
    {
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            player.ChangeState(player.runningState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(player.jumpingState);
            return;
        }
        if(!player.isGrounded)
        {
            player.ChangeState(player.inAirState);
            return;
        }   
    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }
    
    public override void EnterState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
}
