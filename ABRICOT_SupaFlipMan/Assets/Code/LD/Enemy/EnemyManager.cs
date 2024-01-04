using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float maxLife;
    private float currentLife;
    [HideInInspector] public MobCounter mobCounter;
    private Animator animator;
    public ParticleSystem DeathVFX;

    // Start is called before the first frame update
    private void Awake()
    {
        mobCounter = GetComponentInParent<MobCounter>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {
        currentLife = maxLife; 
    }

    public void TakeDamage(float damage, float hitStunTime)
    {
        animator.SetTrigger("gotHit");

        if (GetComponentInChildren<EnemyBehaviour>())
            GetComponentInChildren<EnemyBehaviour>().StunState(hitStunTime);

        else if (GetComponentInChildren<ShooterBehaviour>())
            GetComponentInChildren<ShooterBehaviour>().StunState(hitStunTime);

        currentLife -= damage;
        if (currentLife <= 0 )
        {
            Instantiate(DeathVFX, transform.position, Quaternion.identity);
            AudioManager.audioInstance.PlaySound(18);
            Destroy(gameObject);
            mobCounter.EnemyDead();
        }
    }

    public void Kill()
    {
        if (currentLife > 0)
        {
            currentLife = 0;
            Instantiate(DeathVFX, transform.position, Quaternion.identity);
            AudioManager.audioInstance.PlaySound(18);
            Destroy(gameObject);
            mobCounter.EnemyDead();

        }
    }
}
