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

        return player.climbingState;
    }
    
    public override void EnterState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
    
}
