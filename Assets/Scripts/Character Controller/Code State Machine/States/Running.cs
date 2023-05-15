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
        //player.rb.velocity = new Vector3(Input.GetAxis("Horizontal") * player.speed * Time.deltaTime, player.rb.velocity.y);
        player.rb.MovePosition(player.rb.position + new Vector3(Input.GetAxis("Horizontal") * player.speed * Time.deltaTime, 0, 0));
        if(Input.GetKeyDown(KeyCode.Space))
        {
            return player.jumpingState;
        }
        if(!player.isGrounded)
        {
            return player.inAirState;
        }
        return player.runningState;
    }

    public override void EnterState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }
}
