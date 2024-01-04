using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab2Transition : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackManager.instance.canReceiveInput = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackManager.instance.inputReceived == AttackManager.AttackInput.A && stateInfo.normalizedTime < AttackManager.instance.timeToReceiveInput)
        {
            AttackManager.instance.playerMovement.canMove = false;
            animator.SetTrigger("A");
            AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        }
        else if(AttackManager.instance.inputReceived == AttackManager.AttackInput.B && stateInfo.normalizedTime < AttackManager.instance.timeToReceiveInput)
        {
            AttackManager.instance.playerMovement.canMove = false;
            animator.SetTrigger("B");
            AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        }
    }
}
