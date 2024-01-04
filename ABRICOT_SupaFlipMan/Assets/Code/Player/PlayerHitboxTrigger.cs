using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxTrigger : MonoBehaviour
{
    enum ImpactJabSounds
    {
        ImpactJab1,
        ImpactJab2,
        ImpactJab3,
    }

    public float hitStunTime = 0.5f;

    private PlayerManager manager;
    public ParticleSystem hitVFX;

    private void Start()
    {
        manager = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitVFX, other.ClosestPoint(transform.position), Quaternion.identity);
        manager.PlaySFX();
        if (other.gameObject.TryGetComponent(out EnemyManager enemyManager))
        {
            print(manager.currentAttack.ToString());
            enemyManager.TakeDamage(manager.damage, hitStunTime);
            manager.AddCombo();
            if (manager.currentAttack == PlayerManager.CurrentAttack.RKICK)
            {
                Vector3 direction = manager.transform.forward;
                enemyManager.GetComponent<Rigidbody>().AddForce(direction * manager.reverseKickForce, ForceMode.VelocityChange);
            }

                
        }                                                                                                                   
        else if(other.gameObject.TryGetComponent(out PushBox pushBox) && other.gameObject.GetComponent<LiftObject>().isGrounded)
        {
            pushBox.BumpBox(manager.transform.position);
        }

        //(o.luanda)
        if(other.gameObject.TryGetComponent(out Barrel barrel) && !barrel.isMoving)
        {
            Vector3 direction =  new Vector3(other.gameObject.transform.position.x - manager.transform.position.x,  0, other.gameObject.transform.position.z - manager.transform.position.z).normalized;
            barrel.MoveBarrel(direction);
        }
    }
} 
