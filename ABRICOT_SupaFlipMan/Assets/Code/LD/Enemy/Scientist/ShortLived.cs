using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortLived : MonoBehaviour
{
    public float LifeTime = 2f;
    private AcidManager acidManager;
    private AcidEvent acidEvent;
    private void Start()
    {
        acidManager = transform.parent.GetComponent<AcidManager>();
        acidEvent = GetComponent<AcidEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            if (acidEvent.playerTrigger == true)
            {
                acidManager.PlayerExitAcid();
            }
            Destroy(gameObject);
        }
    }
}
