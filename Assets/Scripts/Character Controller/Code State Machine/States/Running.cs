using System;
using Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Running : State
{
    public override void UpdateState(PlayerStateManager player)
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            player.ChangeState(player.idleState);
            return;
        }

        float acceleration = Input.GetAxis("Horizontal") * player.runAcceleration * Time.deltaTime;
        if ((acceleration > 0  && player.rb.velocity.x < player.runMaxSpeed) || 
            (acceleration < 0 && player.rb.velocity.x > -player.runMaxSpeed))
            player.rb.velocity += new Vector3(acceleration, 0, 0);

        
        if(Input.GetKeyDown(KeyCode.Space))
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
