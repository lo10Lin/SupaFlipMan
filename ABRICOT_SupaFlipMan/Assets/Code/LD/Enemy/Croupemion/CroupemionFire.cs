using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroupemionFire : MonoBehaviour
{
    public GameObject kunai;
    public Transform shootPoint;
    public Transform board;
    private PlayerManager playerManager;
    public float bulletSpeed;
    public float damage;
    private GameObject[] kunaiInstantiate;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        kunaiInstantiate = new GameObject[3];
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void throwKunai()
    {
        dir = playerManager.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        dir.Normalize();
        AudioManager.audioInstance.PlaySound(20);
        for (int i = 0; i < 3; i++)
        {
            kunaiInstantiate[i] = Instantiate(kunai, shootPoint.position, Quaternion.identity , board) as GameObject;
            kunaiInstantiate[i].GetComponent<Transform>().Rotate( 90f, targetAngle, 0f);
        }

        kunaiInstantiate[0].GetComponent<KunaiProjectile>().InitKunai(dir, bulletSpeed, damage, 0);
        kunaiInstantiate[1].GetComponent<KunaiProjectile>().InitKunai(dir, bulletSpeed, damage, 30);
        kunaiInstantiate[2].GetComponent<KunaiProjectile>().InitKunai(dir, bulletSpeed, damage, -30);
    }
}
