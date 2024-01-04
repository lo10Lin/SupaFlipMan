using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : MonoBehaviour
{
    public CapsuleCollider katanaHitbox;
    public ParticleSystem katanaVFX;
    public float defectRecoverTime = 0.3f;
    public float deflectSpeedMultiplier = 1.1f;
    private EnemiesAnimEvent enemiesAnimEvent;
    private Animator animator;
    private EnemyBehaviour enemyBehaviour;
    private Transform player;
    
    void Awake()
    {
        enemiesAnimEvent = transform.parent.GetComponentInChildren<EnemiesAnimEvent>();
        animator = transform.parent.GetComponentInChildren<Animator>();
        enemyBehaviour = transform.parent.GetComponentInChildren<EnemyBehaviour>();
        enemiesAnimEvent.KatanaHitbox = katanaHitbox;
        enemiesAnimEvent.KatanaVFX = katanaVFX;
        player = FindObjectOfType<PlayerManager>().transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemyBehaviour.currentState != EnemyBehaviour.State.Stunned)
        {
            other.gameObject.layer = LayerMask.NameToLayer("Projectile");
            AudioManager.audioInstance.PlaySound(64);
            animator.SetTrigger("Deflect");
            enemyBehaviour.StunState(defectRecoverTime, EnemyBehaviour.State.Deflecting);
        
            if (other.TryGetComponent(out ShurikenProjectile shuriken))
            {
                Vector3 dir = player.position - shuriken.transform.position;
                dir.Normalize();
                shuriken.dir = new Vector3(dir.x, shuriken.dir.y, dir.z);
                shuriken.speed *= deflectSpeedMultiplier;
            }
            if (other.TryGetComponent(out VialProjectile vial))
            {
                vial.currentFallTime = vial.fallTime + Time.time;
                Vector3 dir = player.position - vial.transform.position;
                dir.Normalize();
                vial.dir = new Vector3(dir.x, vial.dir.y, dir.z);
                vial.speed *= deflectSpeedMultiplier;
            }
            if (other.TryGetComponent(out KunaiProjectile kunai))
            {
                kunai.transform.rotation = Quaternion.identity;
                Vector3 dir = player.position - kunai.transform.position;
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                dir.Normalize();
                kunai.dir = new Vector3(dir.x, kunai.dir.y, dir.z);
                kunai.GetComponent<Transform>().Rotate(90f, targetAngle, 0f);
                kunai.speed *= deflectSpeedMultiplier;
            }
        }
    }
}
