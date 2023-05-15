using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAir : State
{
    public override State UpdateState(PlayerStateManager player)
    {
        if (player.isGrounded)
        {
            return player.idleState;
        }
        InAirMovement(player);
        return player.inAirState;
    }

    public override void EnterState(PlayerStateManager player)
    {
    }

    public override void ExitState(PlayerStateManager player)
    {
    }

    void InAirMovement(PlayerStateManager player)
    {
        player.rb.MovePosition(player.rb.position + new Vector3(Input.GetAxis("Horizontal") * player.speed * Time.deltaTime, 0, 0));
    }
}
