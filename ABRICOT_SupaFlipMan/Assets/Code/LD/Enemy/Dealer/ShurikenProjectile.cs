using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenProjectile : MonoBehaviour
{
    private float dmg;
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float speed;
    private Rigidbody rb;
    private float hitStunTime = 1f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out PlayerManager player))
        {
            player.HitPlayer(dmg);
            AudioManager.audioInstance.PlaySound(19);
        }

        else if (other.gameObject.TryGetComponent(out EnemyManager enemy) && !other.gameObject.GetComponentInChildren<Ninja>())
        {
            enemy.TakeDamage(dmg, hitStunTime);
            AudioManager.audioInstance.PlaySound(19);
        }
        else
        {
            AudioManager.audioInstance.PlaySound(36);
        }

        Destroy(gameObject);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(10f, 0f, 0f));
        rb.velocity = speed * Time.fixedDeltaTime *  new Vector3(dir.x, rb.velocity.y, dir.z);
    }

    public void InitBullet(Vector3 direction, float mSpeed, float damage)
    {
        dir = direction;    
        speed = mSpeed;
        dmg = damage;
    }

}
