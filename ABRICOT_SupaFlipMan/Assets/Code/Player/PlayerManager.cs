using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour
{
    public Animator UILife;
    public float maxHp = 10f;
    public float damage = 1.0f;
    public Animator UICombo;
    public Text numCombo;
    public float speedBlink = .1f;
    public float timeInvincibility = 1f;
    public float combo = 0f;
    [HideInInspector] public float maxCombo = 0;

    [SerializeField]  private float currentHp;
    public bool isInvincible = false;
    private Renderer[] playerRenderer;

    public Text timer;
    [HideInInspector] public float myTimer;
    private float TimeStart;
    private readonly float loadingTime = 2.3f;

    public float timeInAcid = 0;
    public float reverseKickForce = 10f;

    public Animator transition;
    public GameObject gameOverScreen;
    public GameObject InGameUI;

    private PlayerMovement playerMovement;
    private CapsuleCollider playerCollider;

    private Animator playerAnimator;
    private Rigidbody playerRb;

    [HideInInspector]public CurrentAttack currentAttack;
    [HideInInspector]public enum CurrentAttack
    {
        JAB,
        HOOK,
        KICK,
        RKICK,
        SLASH,
        HEAVYATTACK,
        STOMP,
    };


    // Start is called before the first frame update
    void Awake()
    {
        playerRenderer = GetComponentsInChildren<Renderer>();
        playerAnimator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerRb = GetComponent<Rigidbody>();
        currentHp = maxHp;
    }

    private void Start()
    {
        EventManager.instance.addPlayerLife += HealPlayer;
        EventManager.instance.removePlayerLife += HitPlayer;
        StartCoroutine(WaitLoadTime());
        TimeStart = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateChrono();
         if (Input.GetKeyDown(KeyCode.J))
        {
            HitPlayer(1f);
            Debug.Log(currentHp);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            AddCombo();
        }
    }

    #region HP
    public void HealPlayer()
    {
        if (currentHp == maxHp)
        {
            return;
        }
        else
        {
            UILife.SetTrigger("AddOne");
            currentHp += 1;
        }
    }
    enum PlayerHitSounds
    {
        HIT1,
        HIT2,
        HIT3,
    };

    private void PlayerHitSound()
    {
            PlayerHitSounds hit = (PlayerHitSounds)Random.Range(0, 3);
            AudioManager.audioInstance.PlaySound((int)hit + 37);
    }
    public void HitPlayer(float damage)
    {
        if (!isInvincible)
        {
            currentHp -= damage;
            isInvincible = true;
            if (currentHp <= 0f)
            {
                AudioManager.audioInstance.StopAllSounds();
                AudioManager.audioInstance.PlaySound(33);
                playerMovement.enabled = false;
                Debug.Log("You are dead");
                playerAnimator.SetTrigger("isDeath");
                playerRb.isKinematic = true;
                StartCoroutine(GameOver());
            }
            else
            {
                PlayerHitSound();
                StopAllCoroutines();
                StartCoroutine(BlinkPlayer());
                StartCoroutine(HandleBlinkDelay());
                ResetCombo();
            }
            UILife.SetTrigger(currentHp.ToString()); ResetCombo();
        }
    }

    public void AcidHit(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0f && playerMovement.enabled)
        {
            AudioManager.audioInstance.StopAllSounds();
            AudioManager.audioInstance.PlaySound(33);
            playerMovement.enabled = false;
            playerCollider.enabled = false;
            Debug.Log("You are dead");
            playerAnimator.SetTrigger("isDeath");
            playerRb.isKinematic = true;
            StartCoroutine(GameOver());
        }
        else if (playerMovement.enabled)
        {
            ResetCombo();
            PlayerHitSound();
        }
        UILife.SetTrigger(currentHp.ToString());
    }

    public IEnumerator GameOver()
    {
        PauseMenu.canPauseGame = false;
        yield return new WaitForSecondsRealtime(2f);
        InGameUI.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    #endregion

    #region Invincibility

    IEnumerator BlinkPlayer()
    {
        while(isInvincible)
        {
            foreach (Renderer ren in playerRenderer)
            {
                if (ren != null)
                    ren.enabled = false;
            }
            yield return new WaitForSecondsRealtime(speedBlink);
            foreach (Renderer ren in playerRenderer)
            {
                if (ren != null)
                    ren.enabled = true;
            }
            yield return new WaitForSecondsRealtime(speedBlink);
        }
    }

    IEnumerator HandleBlinkDelay()
    {
        yield return new WaitForSecondsRealtime(timeInvincibility);
        isInvincible = false;
        StopCoroutine(BlinkPlayer());
    }
    #endregion

    #region Combo
    public void AddCombo()
    {
        combo++;
        numCombo.text = "" + combo;
        UICombo.SetTrigger("AddScore");
        if(combo > maxCombo)
        {
            maxCombo = combo;
        }
    }

    public void RemoveCombo(int nb)
    {
        AudioManager.audioInstance.PlaySound(53);
        combo -= nb;
        numCombo.text = "" + combo;
        UICombo.SetTrigger("ResetScore");
    }

    public void ResetCombo()
    {
        AudioManager.audioInstance.PlaySound(53);
        combo = 0f;
        numCombo.text = "" + combo;
        UICombo.SetTrigger("ResetScore");

    }
    #endregion

    #region Chrono

    void UpdateChrono ()
    {
        myTimer = Time.time - TimeStart;
        
        string minute = ((int)myTimer / 60).ToString("00.");
        string sec = (myTimer % 60).ToString("00.");
        timer.text = minute + ":" + sec;
    }

    private void ResetChrono()
    {
        TimeStart = Time.time;
    }
    #endregion

    #region ImpactSounds
    enum ImpactJabSounds
    {
        ImpactJab1,
        ImpactJab2,
        ImpactJab3,
    };

    enum ImpactKickSounds
    {
        IMPACTKICK1,
        IMPACTKICK2,
    }
    public void PlaySFX()
    {
        switch (currentAttack)
        {
            case CurrentAttack.JAB:
                ImpactJabSounds impactJab = (ImpactJabSounds)Random.Range(0, 3);
                AudioManager.audioInstance.PlaySound((int)impactJab+7);
                break;

            case CurrentAttack.HOOK:
                AudioManager.audioInstance.PlaySound(6);
                break;

            case CurrentAttack.STOMP:
                AudioManager.audioInstance.PlaySound(52);
                break;

            case CurrentAttack.KICK:
            case CurrentAttack.RKICK:
                ImpactKickSounds impactKick = (ImpactKickSounds)Random.Range(0, 2);
                AudioManager.audioInstance.PlaySound((int)impactKick + 31);
                break;
            default:
                break;
        }

    }

    #endregion

    #region Loading
    private IEnumerator WaitLoadTime()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(loadingTime);
        Time.timeScale = 1;
        PauseMenu.canPauseGame = true;
    }
    #endregion
}