using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public static AttackManager instance;
    public PlayerMovement playerMovement;
    public Animator playerAnimator;
    public Rigidbody playerRb;

    public bool canReceiveInput;
    public float timeToReceiveInput = 0.9f;

    public SphereCollider jabHitbox;
    public SphereCollider kickHitbox;
    public SphereCollider dashAttackHitbox;
    public SphereCollider stompHitbox;
    public BoxCollider slideHitbox;

    [HideInInspector] public PlayerManager playerManager;
    [HideInInspector] public PlayerAnimationEvents playerAnimationEvents;

    public enum AttackInput
    {
        None = -1,
        A,
        B,
    }
    public AttackInput inputReceived;

    private void Awake()
    {
        instance = this;
        playerMovement = GetComponent<PlayerMovement>();
        playerManager = GetComponent<PlayerManager>();
        playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();
    }
    public void Update()
    {
        if(canReceiveInput && !PauseMenu.GameIsPaused)
        {
            if(Input.GetButtonDown("Fire1"))
            {
                canReceiveInput = false;
                inputReceived = AttackInput.A;
            }
            else if(Input.GetButtonDown("Fire2"))
            {
                canReceiveInput = false;
                inputReceived = AttackInput.B;
            }
        }
    }

    public void InputManager()
    {
        canReceiveInput = !canReceiveInput;
    }

}
