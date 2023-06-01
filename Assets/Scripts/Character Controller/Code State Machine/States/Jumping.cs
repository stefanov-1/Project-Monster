using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jumping : State
{
    PlayerStateManager player;
    public override void UpdateState(PlayerStateManager player)
    {
        if(player.isGrounded)
        {
            player.ChangeState(player.idleState);
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
        player.rb.AddForce(new Vector3(0, player.jumpForce, 0), ForceMode.Impulse);

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
