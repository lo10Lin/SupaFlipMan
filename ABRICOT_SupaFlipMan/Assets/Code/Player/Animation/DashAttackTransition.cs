using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackTransition : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDashAttacking", true);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isSliding", false);
        animator.SetBool("isDashAttacking", false);
    }
}
