using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem DashFeedback;
    private Rigidbody rb;
    private Animator animator;
    private PlayerAnimationEvents playerAnimationEvents;

    public bool canMove = true;
    public float playerSpeed = 8f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float slideCooldown = 3f;
    public float slideSpeed = 13f;
    private float timeToSlideAgain;
    private bool canSlide = true;

    [HideInInspector] public bool speedBoostActive = false;

    [HideInInspector] public Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        playerAnimationEvents = GetComponentInChildren<PlayerAnimationEvents>();
    }

    void Update()
    {
        if(!canSlide && Time.time >= timeToSlideAgain)
        {
            canSlide = true;
            DashFeedback.Play();
        }
        if(canMove)
        {
            if (!PauseMenu.GameIsPaused)
            {
                if (!animator.GetBool("isSliding"))
                {
                    direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
                    animator.SetFloat("speed", direction.magnitude);

                    if (Input.GetButtonDown("Fire3") && canSlide) // Slide is button pressed
                    {
                        Slide();
                    }

                    if (direction.magnitude >= 0.1f)
                    {
                        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                        transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    }
                }

            }
        }
        else if (Input.GetButtonDown("Fire3") && !animator.GetBool("isSliding") && canSlide) // Slide button is pressed while player is attacking
        {
            playerAnimationEvents.DisableHiboxes();
            Slide();
        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void FixedUpdate()
    {
        if (!animator.GetBool("isSliding"))
        {
            rb.velocity = new Vector3(direction.x * playerSpeed, rb.velocity.y, direction.z * playerSpeed);
        }

    }

    private void Slide()
    {
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Box"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Acid"), true);
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Projectile"), true);

        animator.SetTrigger("Slide");
        animator.SetBool("isSliding", true);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

        Vector3 dashDirection;
        if (direction != Vector3.zero)
            dashDirection = direction;
        else
            dashDirection = transform.forward;

        canSlide = false;
        timeToSlideAgain = Time.time + slideCooldown;
        rb.velocity = new Vector3(dashDirection.x * slideSpeed, rb.velocity.y, dashDirection.z * slideSpeed);
    }

    public void SpeedBoost(float speedMultiplier, float speedBoostDuration)
    {
        StartCoroutine(BoostCoroutine(speedMultiplier, speedBoostDuration));
    }

    private IEnumerator BoostCoroutine(float speedMultiplier, float speedBoostDuration)
    {
        playerSpeed *= speedMultiplier;
        speedBoostActive = true;
        yield return new WaitForSecondsRealtime(speedBoostDuration);
        playerSpeed /= speedMultiplier;
        speedBoostActive = false;
    }
}
