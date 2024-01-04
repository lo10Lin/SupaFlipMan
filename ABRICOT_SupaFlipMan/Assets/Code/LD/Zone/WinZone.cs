using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public Animator transition;
    public GameObject InGameUI;
    public WinScreenControls WinScreen;
    public ParticleSystem particule;

    private PlayerManager playerManager;

    private void OnTriggerEnter(Collider other)
    {
        PauseMenu.canPauseGame = false;
        other.GetComponent<PlayerMovement>().enabled = false;
        playerManager = other.GetComponent<PlayerManager>();
        WinScreen.InitScore(playerManager.myTimer, playerManager.maxCombo);
        playerManager.enabled = false;

        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.transform.rotation = Quaternion.Euler(new Vector3(0f,180f,0f));

        Instantiate(particule, new Vector3(transform.position.x-8, transform.position.z, transform.position.y+5), Quaternion.identity);
        Instantiate(particule, new Vector3(transform.position.x+6, transform.position.y, transform.position.z - 5), Quaternion.identity);
        Instantiate(particule, new Vector3(transform.position.x - 8, transform.position.y, transform.position.z - 5), Quaternion.identity);
        Instantiate(particule, new Vector3(transform.position.x+6, transform.position.y, transform.position.z+5), Quaternion.identity);

        AudioManager.audioInstance.StopAllSounds();
        AudioManager.audioInstance.PlaySound(70);

        StartCoroutine(PlayWinScreen());
        
        other.GetComponentInChildren<Animator>().SetTrigger("Dance");
    }
    IEnumerator PlayWinScreen()
    {
        InGameUI.SetActive(false);
        yield return new WaitForSecondsRealtime(2f);
        WinScreen.gameObject.SetActive(true);
    }
}
