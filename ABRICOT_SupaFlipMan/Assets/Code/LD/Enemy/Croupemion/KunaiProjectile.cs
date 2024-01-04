using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiProjectile : MonoBehaviour
{
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public float speed = 20;
    private float damage;
    private Rigidbody rb;
    private float hitStunTime = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 5, 0);
        rb.velocity = speed * Time.fixedDeltaTime * new Vector3(dir.x, rb.velocity.y, dir.z);
    }
    public void InitKunai(Vector3 mDir, float mSpeed, float mDamage, float mAngle)
    {
        dir = Quaternion.Euler(new Vector3(0f, mAngle, 0f)) * mDir;
        speed = mSpeed;
        damage = mDamage;
        transform.Rotate(0f, 0f, -mAngle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            player.HitPlayer(damage);
            AudioManager.audioInstance.PlaySound(19);
        }
        else if (collision.gameObject.TryGetComponent(out EnemyManager enemy) && !collision.gameObject.GetComponentInChildren<Ninja>())
        {
            enemy.TakeDamage(damage, hitStunTime);
            AudioManager.audioInstance.PlaySound(19);
        }
        else
        {
            AudioManager.audioInstance.PlaySound(36);
        }
        Destroy(gameObject);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.identity;
    }
}
