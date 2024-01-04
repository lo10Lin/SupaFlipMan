using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostEvent : MonoBehaviour
{
    public float sppedBoostMultiplier = 1.5f;
    public float sppedBoostDuration = 8f;
    public int comboNumberToTrade = 10;

    public Image heal;
    public Image speed;
    private Image currentImage;
    private Animator animator;
    private PlayerManager player;
    private PlayerMovement playerMovement;
    private readonly float clickCooldown = 0.4f;
    private readonly float timeToSwitch = 0.15f;
    private float timeToClickAgain = 0f;
    private float boostTime;
    private bool canSwitch = true;
    void Awake()
    {
        player = FindObjectOfType<PlayerManager>();
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        currentImage = heal;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMovement.speedBoostActive) // Animation when speedboost active
        {
            speed.fillAmount = (boostTime - Time.time) / sppedBoostDuration;
        }

        if (Input.GetButton("Jump") && Time.time >= timeToClickAgain) // Button hold, fill, activate boost
        {
            if (currentImage == heal) //Heal
            {
                currentImage.fillAmount += Time.deltaTime;
                if (currentImage.fillAmount >= 1f && player.combo >= comboNumberToTrade)
                {
                    currentImage.fillAmount = 0f;
                    player.RemoveCombo(comboNumberToTrade);
                    player.HealPlayer();
                    AudioManager.audioInstance.PlaySound(40);
                }
                else if (canSwitch && currentImage.fillAmount >= timeToSwitch)
                    canSwitch = false;
            }

            else if(!playerMovement.speedBoostActive) //SpeedBoost
            {
                currentImage.fillAmount += Time.deltaTime;
                if (currentImage.fillAmount >= 1f && player.combo >= comboNumberToTrade)
                {
                    boostTime = Time.time + sppedBoostDuration;
                    player.RemoveCombo(comboNumberToTrade);
                    playerMovement.SpeedBoost(sppedBoostMultiplier, sppedBoostDuration);
                }
                else if (canSwitch && currentImage.fillAmount >= timeToSwitch)
                    canSwitch = false;
            }
            
        }

        else if (Input.GetButtonUp("Jump") &&  canSwitch && Time.time >= timeToClickAgain) // Switch
        {
            currentImage.fillAmount = 0f;
            animator.SetBool("Switch", !animator.GetBool("Switch"));
            timeToClickAgain = Time.time + clickCooldown;
            if (animator.GetBool("Switch"))
                currentImage = speed;
            else
                currentImage = heal;
        }

        else if (Input.GetButtonUp("Jump"))
        {
            canSwitch = true;
            if(currentImage == heal || !playerMovement.speedBoostActive)
                currentImage.fillAmount = 0f;
        }
    }
}
