using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour
{
    private Animator animator;
    private Vector3 direction;

    private readonly float turnSmoothTime = 0.05f;
    private float turnSmoothVelocity;
    private Rigidbody rb;
    private PhysicMaterial pm;
    private float originalFriction;

    private void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
        rb = transform.parent.GetComponent<Rigidbody>();
        pm = transform.parent.GetComponent<Collider>().material;
        originalFriction = pm.dynamicFriction;
        print(originalFriction);
    }
    public enum State
    {
        Idle,
        Shooting,
        Stunned,
    }

    public State currentState = State.Idle;

    private void OnTriggerStay(Collider other)
    {
        switch (currentState)
        {
            case State.Idle:

                animator.SetTrigger("Shoot");
                currentState = State.Shooting;
                break;

            case State.Shooting:
                direction = other.transform.position - transform.position;
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.parent.rotation = Quaternion.Euler(0f, angle, 0f);
                break;

            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(currentState != State.Stunned)
        {
            animator.SetTrigger("Idle");
            currentState = State.Idle;
        }
    }

    public void StunState(float hitStunTime)
    {
        StopAllCoroutines();
        StartCoroutine(StunCoroutine(hitStunTime));
    }

    private IEnumerator StunCoroutine(float hitStunTime)
    {
        currentState = State.Stunned;
        pm.dynamicFriction = pm.staticFriction = 0.6f;
        yield return new WaitForSecondsRealtime(hitStunTime);
        rb.velocity = Vector3.zero;
        pm.dynamicFriction = pm.staticFriction = originalFriction;
        if (currentState == State.Idle)
            animator.SetTrigger("Idle");
        else
            currentState = State.Idle;
    }
}
