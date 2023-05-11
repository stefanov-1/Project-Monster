using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Entered state " + stateInfo.ToString());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat("MovementSpeed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("Exited state " + stateInfo.ToString());
    }
}
