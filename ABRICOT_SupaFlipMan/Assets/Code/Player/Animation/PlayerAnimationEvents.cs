using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public Rigidbody playerRb;
    public PlayerMovement playerMovement;

    [HideInInspector] public ParticleSystem KatanaVFX;
    [HideInInspector] public CapsuleCollider KatanaHitbox;

    [HideInInspector] public PlayerManager manager;
    public ParticleSystem stompVFX;


    private void Start()
    {
        manager = AttackManager.instance.playerManager;
    }

    #region Hitboxes
    public void EnableJabHitbox()
    {
        AttackManager.instance.jabHitbox.enabled = true;
    }
    public void DisableJabHitbox()
    {
        AttackManager.instance.jabHitbox.enabled = false;
    }

    public void EnableKickHitbox() 
    {
        AttackManager.instance.kickHitbox.enabled = true;
    }
    public void DisableKickHitbox()
    {
        AttackManager.instance.kickHitbox.enabled = false;
    }
    public void EnableDashAttackHitbox()
    {
        AttackManager.instance.dashAttackHitbox.enabled = true;
    }
    public void DisableDashAttackHitbox()
    {
        AttackManager.instance.dashAttackHitbox.enabled = false;
    }

    public void EnableStompHitbox()
    {
        AttackManager.instance.stompHitbox.enabled = true;
    }
    public void DisableStompHitbox()
    {
        AttackManager.instance.stompHitbox.enabled = false;
    }

    public void DisableHiboxes()
    {
        AttackManager.instance.jabHitbox.enabled = false;
        AttackManager.instance.kickHitbox.enabled = false;
        AttackManager.instance.dashAttackHitbox.enabled = false;
        AttackManager.instance.stompHitbox.enabled = false;
    }

    #endregion

    #region Katana
    public void EnableVFX()
    {
        if (!KatanaVFX.isPlaying)
        {
            KatanaVFX.Play();
        }
    }

    public void DisableVFX()
    {
        if (KatanaVFX.isPlaying)
        {
            KatanaVFX.Stop();
        }
    }

    public void EnableKatanaHitbox()
    {
        KatanaHitbox.enabled = true;
    }

    public void DisableKatanaHitbox()
    {
        KatanaHitbox.enabled = false;
    }
    #endregion

    #region Sound

    #region Jab
    enum JabSounds
    {
        Jab1,
        Jab2,
        Jab3,
    };

    public void soundJab()
    {
        JabSounds jab = (JabSounds)Random.Range(0, 3);
        AudioManager.audioInstance.PlaySound((int)jab + 10);
    }

    public void changeCurrentAttackToHook()
    {
        manager.currentAttack = PlayerManager.CurrentAttack.HOOK;
    }
    #endregion Jab


    #region Run
    enum RunSounds
    {
        Run1,
        Run2,
        Run3,
        Run4,
    };
    public void soundRun()
    {
        RunSounds run = (RunSounds)Random.Range(0,4);
        AudioManager.audioInstance.PlaySound((int)run + 13);
    }
    #endregion Run

    public void soundSlide()
    {
        AudioManager.audioInstance.PlaySound(17);
    }

    #region Kick 
    
    enum KickSounds
    {
        KICK1,
        KICK2,
    };

    public void soundKick()
    {
        KickSounds kick = (KickSounds)Random.Range(0, 2);
        AudioManager.audioInstance.PlaySound((int)kick + 29);
    }
    #endregion Kick

    public void soundStomp()
    {
        Instantiate(stompVFX, transform.position, Quaternion.identity);
        AudioManager.audioInstance.PlaySound(51);
    }

    public void changeCurrentAttackToRKick()
    {
        manager.currentAttack = PlayerManager.CurrentAttack.RKICK;
    }

    #region Katana 

    enum SlashSounds
    {
        SLASH1,
        SLASH2,
    };
    public void soundSlash()
    {
        SlashSounds slash = (SlashSounds)Random.Range(0, 2);
        AudioManager.audioInstance.PlaySound((int)slash + 23);
    }

    enum SpinSounds
    {
        SPIN1,
        SPIN2,
    };

    public void soundHeavyAttack()
    {
        SpinSounds spin = (SpinSounds)Random.Range(0, 2);
        AudioManager.audioInstance.PlaySound((int)spin + 49);
    }

    #endregion Katana

    #endregion
}
