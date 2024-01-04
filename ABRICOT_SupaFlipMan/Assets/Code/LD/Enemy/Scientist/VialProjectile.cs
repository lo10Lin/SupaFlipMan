using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VialProjectile : MonoBehaviour
{
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float speed;
    [HideInInspector] public float fallTime = 1.5f;
    [HideInInspector] public float currentFallTime;
    private float dmg;
    private Transform acidPf;
    public ParticleSystem acidSplashVFX;
    private Transform parent;

    private Rigidbody rb;
    private CustomGravity gravity;

    private float RandomRotX;
    private float RandomRotY;
    private float RandomRotZ;

    private float hitStunTime = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = GetComponent<CustomGravity>();

        RandomRotX = Random.Range(-7f, 7f);
        RandomRotY = Random.Range(-7f, 7f);
        RandomRotZ = Random.Range(-7f, 7f);
    }
    public void Init(Vector3 direction, float projectileSpeed, float damage, float timeBeforeFalling, Transform acidPrefab, Transform acidManager)
    {
        dir = direction;
        speed = projectileSpeed;
        acidPf = acidPrefab;
        parent = acidManager;
        dmg = damage;
        fallTime = timeBeforeFalling;
        currentFallTime = Time.time + fallTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time >= currentFallTime)
        {
            gravity.gravityScale = 10;
        }
        else
        {
            rb.velocity = speed * Time.fixedDeltaTime * 2 * dir;
        }

        transform.Rotate(new Vector3(RandomRotX, RandomRotY, RandomRotZ));
    }


    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out PlayerManager player))
            player.HitPlayer(dmg);

        else if(other.gameObject.TryGetComponent(out EnemyManager enemy) && !other.gameObject.GetComponentInChildren<Ninja>())
            enemy.TakeDamage(dmg , hitStunTime);

        Instantiate(acidPf, new Vector3 (transform.position.x, acidPf.position.y, transform.position.z), acidPf.rotation, parent);
        Instantiate(acidSplashVFX, new Vector3 (transform.position.x, acidPf.position.y, transform.position.z), Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
    }
}
