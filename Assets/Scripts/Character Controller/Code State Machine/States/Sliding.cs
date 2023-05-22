using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : State
{
    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.ChangeState(player.jumpingState);
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
