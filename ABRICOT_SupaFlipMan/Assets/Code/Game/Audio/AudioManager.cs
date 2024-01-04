using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    [HideInInspector] public bool isFistTimePlay = true;
    public static AudioManager audioInstance;
    // Start is called before the first frame update
    void Awake()
    {
        
        if (audioInstance == null)
        {
            audioInstance = this;
        }

        foreach (Sound s in sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();


            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
            s.Source.playOnAwake = s.PlayOnAwake;
        }
        PlaySound(67);
    }

    private void Update()
    {
        if (sounds[3].Source.isPlaying)
        {
            VolumeSound(68, 0.1f);
        }
        else
        {
            VolumeSound(68, 0.3f);
        }
        if (!sounds[67].Source.isPlaying && !sounds[68].Source.isPlaying && isFistTimePlay)
        {
            PlaySound(68);
            isFistTimePlay = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StopAllSounds();
        }
    }
    public void PlaySound(int soundID)
    {

        if (soundID >= sounds.Length)
        {
            Debug.LogWarning("Sound: " + soundID + " not found the biggest index posisible is " + (sounds.Length - 1) );
            return;
        }
        else
        {
            Debug.Log(sounds[soundID].Name);
        }
        if (sounds[soundID].Source != null)
            sounds[soundID].Source.Play();
    }

    public void StopSound(int soundID)
    {
        if (soundID >= sounds.Length)
        {
            Debug.LogWarning("Sound: " + soundID + " not found the biggest index posisible is " + (sounds.Length - 1));
            return;
        }
        if (sounds[soundID].Source != null)
            sounds[soundID].Source.Stop();
    }

    public void VolumeSound(int soundID, float newVolume)
    {
        if (soundID >= sounds.Length)
        {
            Debug.LogWarning("Sound: " + soundID + " not found the biggest index posisible is " + (sounds.Length - 1));
            return;
        }
        if (sounds[soundID].Source != null)
            sounds[soundID].Source.volume = newVolume;
    }

    public void StopAllSounds()
    {
        Debug.LogWarning(sounds.Length);
        for(int i = 0; i < sounds.Length;i++)
        {
            if (sounds[i].Source.isPlaying)
            {
                Debug.LogWarning(sounds[i].Name);
                StopSound(i);
            }
        }
    }
}
