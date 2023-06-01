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

        if (Vector3.Distance(player.rb.position, ControlValues.Instance.currentSlideEnd) < 0.5f)
        {
            player.ChangeState(player.idleState);
            return;
        }
        
        player.rb.velocity = ControlValues.Instance.currentSlideDirection * player.slideSpeed;

    }

    public override void FixedUpdateState(PlayerStateManager player)
    {
        
    }
    
    public override void EnterState(PlayerStateManager player)
    {
        player.rb.velocity = Vector3.zero;

        Vector3 closetsPoint = Utils.ClosestPointOnLineSegment(
            ControlValues.Instance.currentSlideStart,
            ControlValues.Instance.currentSlideEnd,
            player.rb.position);

        player.rb.position = closetsPoint; // snap the player to the clmbable surface

        player.rb.useGravity = false;
        
        ControlValues.Instance.targetMeshRotation = Quaternion.LookRotation(ControlValues.Instance.currentSlideDirection, ControlValues.Instance.currentSurfaceNormal);
    }

    public override void ExitState(PlayerStateManager player)
    {
        player.rb.useGravity = true;

        Vector3 horizontalDirection = new Vector3(Mathf.Round(ControlValues.Instance.currentSlideDirection.x), 0, 0);
        player.rb.AddForce(horizontalDirection * player.slideExitLaunchForce, ForceMode.Impulse);
    }
    
    
}
