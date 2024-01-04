using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out LiftEnemy liftEnemy))
        {
            AudioManager.audioInstance.PlaySound(47);
            liftEnemy.Lift();
        }
        else if (other.TryGetComponent(out LiftObject liftObject))
        {
            AudioManager.audioInstance.PlaySound(48);
            liftObject.Lift();
        }
    }
}

