using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            //return player.runningState;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Switching from Jumping State to Idle State");
            return player.idleState;
        }
        return null;
    }

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Jumping State");
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Jumping State");
    }
}
