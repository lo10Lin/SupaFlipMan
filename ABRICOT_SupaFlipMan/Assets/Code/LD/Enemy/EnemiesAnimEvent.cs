using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAnimEvent : MonoBehaviour
{
    [SerializeField]private BoxCollider[] hitBoxes;

    [HideInInspector] public CapsuleCollider KatanaHitbox;
    [HideInInspector] public ParticleSystem KatanaVFX;
    private void Awake()
    {
        hitBoxes = transform.parent.GetComponentsInChildren<BoxCollider>();
    }

    #region Letske
    public void EnableSimpleHitBox()
    {
        hitBoxes[0].enabled = true;
    }

    public void DisableSimpleHitBox()
    {
        hitBoxes[0].enabled = false;
    }

    public void DisableHitBoxes()
    {
        foreach (BoxCollider hit in hitBoxes)
        {
            hit.enabled = false;
        }

        if(KatanaHitbox)
            KatanaHitbox.enabled = false;
        if(KatanaVFX)
            KatanaVFX.Stop();

    }
    #endregion

    #region Letske sounds

    enum LetskeRun
    {
        RUN1,
        RUN2,
        RUN3,
    };

    public void soundLetskeRun()
    {
        LetskeRun run = (LetskeRun)Random.Range(0, 3);
        AudioManager.audioInstance.PlaySound((int)run + 25);
    }
    #endregion Letske sounds

    #region Ninja 

    public void EnableKatanaHitBox()
    {
        KatanaHitbox.enabled = true;
    }

    public void DisableKatanaHitBox()
    {
        KatanaHitbox.enabled = false;
    }

    public void EnableKatanaVFX()
    {
        KatanaVFX.Play();
    }

    public void DisableKatanaVFX()
    {
        KatanaVFX.Stop();
    }


    #endregion

    #region Ninja Run
    enum NinjaRunSounds
    {
        RUN1,
        RUN2, 
        RUN3,
        RUN4,

    };
    public void soundNinjaRun()
    {
        NinjaRunSounds run = (NinjaRunSounds)Random.Range(0, 4);
        AudioManager.audioInstance.PlaySound((int)run + 57);
    }

    enum NinjaSlashSounds
    {
        SLASH1,
        SLASH2,
    };
    public void soundNinjaSlash()
    {
        NinjaSlashSounds slash = (NinjaSlashSounds)Random.Range(0, 2);
        AudioManager.audioInstance.PlaySound((int)slash + 61);
    }
    #endregion Ninja Run
}
