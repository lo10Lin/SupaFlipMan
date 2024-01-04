using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{

   [HideInInspector] public int playerLives;
   [HideInInspector] public int playerBeginningLives = 5;

   [HideInInspector] public int playerScore = 0;
   [HideInInspector] public PlayerControl playerControlRef;

   public AudioClip impactSound;
   public AudioClip hitSound;
    AudioSource audioSource;
  



    void Start()
    {
       playerLives = playerBeginningLives;
       playerScore = 0;

#if false
        playerControlRef = GameObject.Find("Player").GetComponent<PlayerControl>();
#endif



        
    }

    void Update()
    {

    }

    public void ResetPlayer()
    {
        playerLives = playerBeginningLives;
        playerScore = 0;
    }

    void SetPlayerLives(int livesSelected)
    {
        playerLives = livesSelected;
    }


    void SetScore()
    {
        playerScore = 0;
    }

}
