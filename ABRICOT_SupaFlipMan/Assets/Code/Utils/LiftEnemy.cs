using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftEnemy : MonoBehaviour
{
    private Transform enemiesTransform;
    public LayerMask groundLayer;
    private Rigidbody rb;

    public float liftForce = 12f;
    public bool isGrounded = false;

    private SphereCollider[] triggerZones;
    private CapsuleCollider HurtBox;
    private Animator animator;
    private EnemyBehaviour enemyBehaviour;
    private ShooterBehaviour shooterBehaviour;
    public Transform groundCheck;
    private void Start()
    {
        enemiesTransform = transform.parent;
        rb = GetComponent<Rigidbody>();
        triggerZones = GetComponentsInChildren<SphereCollider>();
        animator = GetComponentInChildren<Animator>();
        enemyBehaviour = GetComponentInChildren<EnemyBehaviour>();
        HurtBox = GetComponent<CapsuleCollider>();
        if (enemyBehaviour == null)
        {
            shooterBehaviour = GetComponentInChildren<ShooterBehaviour>();
            Destroy(enemyBehaviour);
        }
        else
        {
            Destroy(shooterBehaviour);
        }
    }

    private void FixedUpdate()
    {
        if (!isGrounded && Physics.CheckSphere(groundCheck.position, .001f, groundLayer))
        {
            StartCoroutine(DelayedGrouded());
            HurtBox.enabled = true;
            AudioManager.audioInstance.PlaySound(45);
            if (gameObject.CompareTag("Letske"))
            {
                AudioManager.audioInstance.PlaySound(64);
            }

            foreach(SphereCollider s in triggerZones)
            {
                s.enabled = true;
            }
            transform.parent = enemiesTransform;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            animator.SetTrigger("Idle");
            if (enemyBehaviour)
            {
                enemyBehaviour.currentState = EnemyBehaviour.State.Idle;
            }
            else if (shooterBehaviour)
            {
                shooterBehaviour.currentState = ShooterBehaviour.State.Idle;
            }
        }
    }

    public void Lift()
    {
        if (isGrounded)
        {
            isGrounded = false;
            foreach (SphereCollider s in triggerZones)
            {
                s.enabled = false;
            }
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            rb.AddForce(Vector3.up * liftForce, ForceMode.VelocityChange);
            animator.SetTrigger("lifted");

            HurtBox.enabled = false;

            if (enemyBehaviour)
            {
                enemyBehaviour.StopAllCoroutines();
                enemyBehaviour.currentState = EnemyBehaviour.State.Stunned;
            }
            else if(shooterBehaviour)
            {
                shooterBehaviour.StopAllCoroutines();
                shooterBehaviour.currentState = ShooterBehaviour.State.Stunned;
            }    
            transform.parent = null;
        }
    }

    private IEnumerator DelayedGrouded()
    {
        yield return
        isGrounded = true;
    }
}
