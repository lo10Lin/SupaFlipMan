using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobCounter : MonoBehaviour
{
    public Animator mobCounter;
    public Text textNbMob;
    public int nbEnemies = 0;
    public GameObject barrier;
    void Awake()
    {
        nbEnemies = GetComponentsInChildren<EnemyManager>().Length;
        if (nbEnemies <= 0)
        {
            barrier.SetActive(false);
        }
    }

    public void EnemyDead()
    {
        nbEnemies -= 1;
        mobCounter.SetTrigger("Decrease");
        textNbMob.text = nbEnemies.ToString("00.");
        if (nbEnemies <= 0)
        {
            StartCoroutine(OpenDoor());
            mobCounter.SetBool("isFinished", true);
        }
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSecondsRealtime(1f);
        AudioManager.audioInstance.PlaySound(3);
        barrier.SetActive(false);
    }

}
