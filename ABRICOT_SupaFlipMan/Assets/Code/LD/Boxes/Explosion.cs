using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float damage = 1f;
    public float bumpForce = 7f;
    public float hitStunTime = 1f;
    public ParticleSystem vfx;
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(vfx, transform.position, Quaternion.identity);
        if (other.TryGetComponent(out EnemyManager enemyManager))
        {
            Vector3 direction = new Vector3(enemyManager.transform.position.x - transform.position.x, 0, enemyManager.transform.position.z - transform.position.z).normalized;
            other.GetComponent<Rigidbody>().AddForce(direction * bumpForce, ForceMode.VelocityChange);
            enemyManager.TakeDamage(damage, hitStunTime);
        }
        else if(other.TryGetComponent(out PlayerManager playerManager))
        {
            playerManager.HitPlayer(damage);
        }
    }
}
