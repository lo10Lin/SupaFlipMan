using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTransition : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackManager.instance.canReceiveInput = true;
        AttackManager.instance.slideHitbox.enabled = true;
        AttackManager.instance.playerManager.isInvincible = true;
        AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AttackManager.instance.inputReceived == AttackManager.AttackInput.A && stateInfo.normalizedTime < AttackManager.instance.timeToReceiveInput)
        {
            AttackManager.instance.playerMovement.canMove = false;
            animator.SetTrigger("A");
            AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        }

       else if (AttackManager.instance.inputReceived == AttackManager.AttackInput.B && stateInfo.normalizedTime < AttackManager.instance.timeToReceiveInput)
        {
            animator.SetTrigger("B");
            AttackManager.instance.playerManager.currentAttack = PlayerManager.CurrentAttack.KICK;
            AttackManager.instance.playerRb.velocity /= 3;
            AttackManager.instance.inputReceived = AttackManager.AttackInput.None;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Box"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Acid"), false);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), false);

        AttackManager.instance.slideHitbox.enabled = false;
        AttackManager.instance.playerManager.isInvincible = false;
        if (!animator.GetBool("isDashAttacking"))
        {
            animator.SetBool("isSliding", false);
        }
        
    }

}
