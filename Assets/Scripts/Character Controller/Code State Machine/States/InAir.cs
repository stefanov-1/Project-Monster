using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAir : State
{
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.isGrounded)
        {
            player.ChangeState(player.idleState);
            return;
        }
        InAirMovement(player);
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

    void InAirMovement(PlayerStateManager player)
    {
        float acceleration = Input.GetAxis("Horizontal") * player.airAcceleration * Time.deltaTime;
        if ((acceleration > 0  && player.rb.velocity.x < player.airMaxSpeed) || 
            (acceleration < 0 && player.rb.velocity.x > -player.airMaxSpeed))
            player.rb.velocity += new Vector3(acceleration, 0, 0);

        player.ApplyGravity();
    }

}
