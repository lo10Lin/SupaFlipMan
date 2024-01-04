using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public float damage = 1f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerManager player))
            player.HitPlayer(damage);

        else if (other.gameObject.TryGetComponent(out EnemyManager enemy))
            enemy.Kill();
    }
}
