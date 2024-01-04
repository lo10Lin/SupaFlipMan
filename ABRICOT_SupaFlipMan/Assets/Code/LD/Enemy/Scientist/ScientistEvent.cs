using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistEvent : MonoBehaviour
{
    public Transform shootPoint;

    public Transform vialPrefab;
    public Transform shortAcidPrefab;
    public Transform acidManager;
    public Transform board;

    public float projectileSpeed = 200f;
    public float damage = 1f;
    public float timeBeforeFalling = 1.5f;

    public void SpawnVial()
    {
        Transform vial = Instantiate(vialPrefab, shootPoint.position, Quaternion.identity, board);
        vial.GetComponent<VialProjectile>().Init(transform.forward, projectileSpeed, damage, timeBeforeFalling, shortAcidPrefab, acidManager);
    }
}
