using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEvent : MonoBehaviour
{
    private AcidManager acidManager;
    [HideInInspector] public bool playerTrigger = false;
    private void Start()
    {
        acidManager = transform.parent.GetComponent<AcidManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ExplosiveObject obj))
        {
            obj.Boom();
            AudioManager.audioInstance.PlaySound(4);
        }
        else if (other.TryGetComponent(out PlayerManager _))
        {
            if (acidManager.enabled == false)
                acidManager.enabled = true;
            acidManager.PlayerEnterAcid();
            playerTrigger = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager _))
        {
            acidManager.PlayerExitAcid();
            playerTrigger = false;
        }
    }
}
