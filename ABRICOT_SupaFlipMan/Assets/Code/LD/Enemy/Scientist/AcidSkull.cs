using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcidSkull : MonoBehaviour
{
    [HideInInspector] public PlayerManager playerManager;
    public Image SkullFiiled;

    void Start()
    {
        playerManager = AttackManager.instance.playerManager;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SkullFiiled.fillAmount = playerManager.timeInAcid * 2f;
    }
}
