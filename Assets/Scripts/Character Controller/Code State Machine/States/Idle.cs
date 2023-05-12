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
        return player.idleState;
    }

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Idle State");
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Idle State");
    }
}
