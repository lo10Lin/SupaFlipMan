using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXAcidManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.childCount > 0 && !AudioManager.audioInstance.sounds[0].Source.isPlaying)
        {
            AudioManager.audioInstance.PlaySound(0);
        }
        else if (transform.childCount <= 0 && AudioManager.audioInstance.sounds[0].Source.isPlaying)
        {
            AudioManager.audioInstance.StopSound(0);
        }
    }

    private void OnDestroy()
    {
            AudioManager.audioInstance.StopSound(0);
    }
}
