using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidManager : MonoBehaviour
{
    public float acidDamage = 1;
    public readonly float timeToGetHit = 0.5f;

    private readonly float timeBetweenHits = 1f;
    private float currentTimeBetweenHits;
    private PlayerManager playerManager;
    private GameObject acidSkull;
    private List<bool> inAcid = new List<bool>();
    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        acidSkull = FindObjectOfType<AcidSkull>().gameObject;
        currentTimeBetweenHits = timeBetweenHits;
    }

    [System.Obsolete]
    private void Update()
    {
        if (inAcid.Count > 0)
        {
            if(playerManager.timeInAcid < timeToGetHit)
            {
                playerManager.timeInAcid = Mathf.Min(timeToGetHit, playerManager.timeInAcid + Time.deltaTime);
            }
            else 
            {
                if(currentTimeBetweenHits >= timeBetweenHits)
                {
                    currentTimeBetweenHits = 0;
                    playerManager.AcidHit(1);
                }
                else
                {
                    currentTimeBetweenHits += Time.deltaTime;
                }
            }
        }
        else
        {
            playerManager.timeInAcid = Mathf.Max(0, playerManager.timeInAcid - Time.deltaTime);
            currentTimeBetweenHits = timeBetweenHits;

            if (playerManager.timeInAcid == 0)
            {
                acidSkull.SetActive(false);
                enabled = false;
            }
                

        }
    }

    public void PlayerEnterAcid()
    {
        inAcid.Add(true);

        if(inAcid.Count== 1)
            acidSkull.SetActive(true);

    }

    public void PlayerExitAcid()
    {
        inAcid.Remove(true);;
    }
}
