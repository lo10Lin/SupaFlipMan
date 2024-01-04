using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTransition : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackManager.instance.canReceiveInput = true;
        AttackManager.instance.playerMovement.canMove = true;
        AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        animator.ResetTrigger("A");
        animator.ResetTrigger("B");
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("gotWeapon"))
        {
            if (AttackManager.instance.inputReceived == AttackManager.AttackInput.A)
            {
                AttackManager.instance.playerMovement.canMove = false;
                AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.JAB;
                animator.SetTrigger("Jab1");
                AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
            }
            else if (AttackManager.instance.inputReceived == AttackManager.AttackInput.B)
            {
                AttackManager.instance.playerMovement.canMove = false;
                AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.KICK;
                animator.SetTrigger("Kick");
                AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
            }
        }
        else
        {
            if (AttackManager.instance.inputReceived == AttackManager.AttackInput.A)
            {
                AttackManager.instance.playerMovement.canMove = false;
                AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.SLASH;
                animator.SetTrigger("Slash");
                AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
            }
            else if (AttackManager.instance.inputReceived == AttackManager.AttackInput.B)
            {
                AttackManager.instance.playerMovement.canMove = false;
                AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.HEAVYATTACK;
                animator.SetTrigger("HeavyAttack");
                AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
            }
        }
    }
}
