using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitTrigger : MonoBehaviour
{
    public float damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerManager player))
        {
            if (gameObject.CompareTag("Letske"))
            {
                AudioManager.audioInstance.PlaySound(LetskeImpact());
            }
            else if (gameObject.CompareTag("RedKatana"))
            {
                AudioManager.audioInstance.PlaySound(63);
            }
            player.HitPlayer(damage);
        }
    }

    #region Letske Impact
    enum soundLetskeImpact
    {
        IMPACT1,
        IMPACT2,
        IMPACT3,
    };

    private int LetskeImpact()
    {
        soundLetskeImpact slash = (soundLetskeImpact)Random.Range(0, 2);
        return((int)slash + 42);
    }
    #endregion
}
