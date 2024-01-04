using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighKickTransition : StateMachineBehaviour
{
    private float timeToReceiveInput = 0.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackManager.instance.canReceiveInput = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackManager.instance.inputReceived == AttackManager.AttackInput.A && stateInfo.normalizedTime < timeToReceiveInput)
        {
            AttackManager.instance.playerMovement.canMove = false;
            AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.STOMP;
            animator.SetTrigger("A");
            AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        }
    }
}
