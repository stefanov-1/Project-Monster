using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Running : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            return player.idleState;
        }
        player.rb.velocity = new Vector3(Input.GetAxis("Horizontal") * player.speed * Time.deltaTime, player.rb.velocity.y);
        Debug.LogWarning(player.rb.velocity);
        return player.runningState;
    }

    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Entering Running State");
    }

    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exiting Running State");
    }
}
