using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaTransition : StateMachineBehaviour
{

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AttackManager.instance.playerAnimationEvents.DisableKatanaHitbox();
        AttackManager.instance.playerAnimationEvents.DisableVFX();
        animator.gameObject.GetComponent<ResetRotation>().enabled = true;
    }
}
