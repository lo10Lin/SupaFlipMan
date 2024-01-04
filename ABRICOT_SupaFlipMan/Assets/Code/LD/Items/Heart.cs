using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float nbHpAdd = 1;
    private PlayerManager manager;
    private void OnTriggerEnter(Collider other)
    {
        manager = other.GetComponent<PlayerManager>();
        manager.HealPlayer();
        AudioManager.audioInstance.PlaySound(41);
        Destroy(gameObject);
    }
}
