using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerFire : MonoBehaviour
{
    public GameObject shuriken;
    public Transform shootPoint;
    public Transform board;
    private PlayerManager playerManager;
    public float bulletSpeed;
    public float damage;
    private GameObject bulletInstantiate;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();

    }

    public void ThrowShuriken()
    {
        dir = playerManager.transform.position - transform.position;
        dir.Normalize();
        bulletInstantiate = Instantiate(shuriken, shootPoint.position, Quaternion.identity, board) as GameObject;
        AudioManager.audioInstance.PlaySound(35);
        bulletInstantiate.transform.Rotate(new Vector3(0f, 0f, 90f));
        bulletInstantiate.GetComponent<ShurikenProjectile>().InitBullet(dir, bulletSpeed,damage);
    }
}
