using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InAir : State
{
    PlayerStateManager player;
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
        this.player = player;
        PlayerStateManager.Move += OnMove;
        PlayerStateManager.InteractButton += OnInteract;
        PlayerStateManager.JumpButton += OnJump;
    }

    public override void ExitState(PlayerStateManager player)
    {
        PlayerStateManager.Move -= OnMove;
        PlayerStateManager.InteractButton -= OnInteract;
        PlayerStateManager.JumpButton -= OnJump;
    }

    void InAirMovement(PlayerStateManager player)
    {
        float acceleration = player.moveInput.x * player.airAcceleration * Time.deltaTime;
        if ((acceleration > 0  && player.rb.velocity.x < player.airMaxSpeed) || 
            (acceleration < 0 && player.rb.velocity.x > -player.airMaxSpeed))
            player.rb.velocity += new Vector3(acceleration, 0, 0);
        
    }

    private void OnMove(object sender, InputAction.CallbackContext context)
    {
    }

    private void OnJump(object sender, InputAction.CallbackContext context)
    {
    }

    private void OnInteract(object sender, InputAction.CallbackContext context)
    {
    }

}
