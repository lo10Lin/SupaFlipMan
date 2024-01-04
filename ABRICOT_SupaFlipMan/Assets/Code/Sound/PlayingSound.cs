using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSound : MonoBehaviour
{
    private GameData gameDataRef;
    AudioSource audioSource;

    bool mPlay;
    bool toggleChange;
    void Start()
    {
        gameDataRef = GameObject.Find("GameManager").GetComponent<GameData>();
        audioSource = GameObject.Find("GameManager").GetComponent<AudioSource>();


        StartCoroutine(PlayOurSound());


        mPlay = true;
        
    }


    void Update()
    {
        if (mPlay == true && toggleChange == true)
        {
    
            audioSource.Play();

            toggleChange = false;
        }
     
        if (mPlay == false && toggleChange == true)
        {
         
            audioSource.Stop();
     
            toggleChange = false;
        }
    }

    IEnumerator PlayOurSound()
    {
        audioSource.Play();

        yield return new WaitForSeconds(gameDataRef.hitSound.length);
        audioSource.clip = gameDataRef.hitSound;
        audioSource.Play();
    }

    void OnGUI()
    {

        mPlay = GUI.Toggle(new Rect(10, 100, 100, 30), mPlay, "Play Music");

 
        if (GUI.changed)
        {
            toggleChange = true;
        }
    }
}
