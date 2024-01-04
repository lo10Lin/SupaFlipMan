using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRoomTrigger : MonoBehaviour
{
    public CameraTranslation Camera;
    public GameObject Barrier;
    public Animator mobCounter;
    public Text txtNbEnemies;
    public MobCounter enemiesNextRoom;
    private SFXAcidManager sfxAcidNextRoom;
    private Projectile[] canonsNextRoom;
    public float offset = 34;
    public SFXAcidManager acidManagerThisRoom;
    public GameObject allCanon;
    private Projectile[] canonsThisRoom;
    private void Awake()
    {
        if (enemiesNextRoom != null)
        {
            sfxAcidNextRoom = enemiesNextRoom.transform.parent.GetComponentInChildren<SFXAcidManager>();
            canonsNextRoom = enemiesNextRoom.transform.parent.GetComponentsInChildren<Projectile>();
        }
        if(allCanon != null)
            canonsThisRoom = allCanon.GetComponentsInChildren<Projectile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Camera.targetPos = Camera.transform.position + new Vector3(offset, 0f, 0f);
        StartCoroutine(EnableTranslation());
        AudioManager.audioInstance.PlaySound(2);
        Barrier.SetActive(true);
        if (enemiesNextRoom == null)
        {
            //txtNbEnemies.text = "00";
            mobCounter.SetTrigger("noMob");
        }
        else if (enemiesNextRoom.nbEnemies != 0)
        {
            txtNbEnemies.text = enemiesNextRoom.nbEnemies.ToString("00.");
            mobCounter.SetTrigger("Start");
            mobCounter.SetBool("isFinished", false);
        }

        if (acidManagerThisRoom != null)
        {
              Destroy(acidManagerThisRoom);
        }
        if (sfxAcidNextRoom != null)
        {
            sfxAcidNextRoom.enabled = true;
        }

        if (canonsThisRoom != null)
        {
            foreach (Projectile p in canonsThisRoom)
            {
                Destroy(p);
            }
        }
        if(canonsNextRoom != null)
        {
            foreach(Projectile p in canonsNextRoom)
            {
                p.enabled = true;
            }
        }
        

    }

    private IEnumerator EnableTranslation()
    {
        Camera.enabled = true;
        gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        Camera.enabled = false;
        
    }
}
