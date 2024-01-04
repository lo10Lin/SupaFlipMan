using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Transform enemy;
    private Animator enemyAnimator;
    private Rigidbody enemyRb;

    private readonly float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float speed = 3f;
    public float recoverTime = 1.15f;
    private readonly float attackTriggerRange = 1.5f;
    
    private Vector3 direction;

    public enum State
    {
        Idle,
        Chase,
        CanAttack,
        Attacking,
        Stunned,
        Deflecting,
    }

    public State currentState;

    private void Awake()
    {
        currentState = State.Idle;
        enemy = transform.parent;
        enemyAnimator = transform.parent.GetComponentInChildren<Animator>();
        enemyRb = transform.parent.GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        switch(currentState)
        {
            case State.Idle:
                direction = other.transform.position - transform.position;
                if (CanAttack())
                {
                    currentState = State.CanAttack;
                    
                }
                else
                {
                    enemyAnimator.SetTrigger("Chase");
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                direction = other.transform.position - transform.position;
                if (CanAttack())
                    currentState = State.CanAttack;
                else
                    Chase();
                break;

            case State.CanAttack:
                StartCoroutine(Attack());
                currentState = State.Attacking;
                break;
                
            default:
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentState != State.Attacking && currentState != State.Stunned && currentState != State.Deflecting)
        {
            enemyRb.velocity = new Vector3(0, enemyRb.velocity.y, 0);
            enemyAnimator.SetTrigger("Idle");
        }
        currentState = State.Idle;
    }

    private void Chase()
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        enemy.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        enemyRb.velocity = new Vector3( direction.normalized.x * speed, enemyRb.velocity.y, direction.normalized.z * speed);
    }

    private bool CanAttack()
    {
        if (direction.magnitude <= attackTriggerRange && !Physics.GetIgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy")))
            return true;
        else
            return false;
    }
    private IEnumerator Attack()
    {
        enemyRb.velocity = new Vector3(0, enemyRb.velocity.y, 0);
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        enemyAnimator.SetTrigger("Attack");
        yield return new WaitForSecondsRealtime(recoverTime);  

        if(currentState == State.Idle) { enemyAnimator.SetTrigger("Idle"); }
        else { currentState = State.Idle; }
        
    }

    public void StunState(float hitStunTime, State state = State.Stunned)
    {
        StopAllCoroutines();
        StartCoroutine(StunCoroutine(hitStunTime, state));
    }

    private IEnumerator StunCoroutine(float hitStunTime, State state)
    {
        enemyRb.velocity = new Vector3(0, enemyRb.velocity.y, 0);
        currentState = state;
        yield return new WaitForSecondsRealtime(hitStunTime);
        if (currentState == State.Idle)
            enemyAnimator.SetTrigger("Idle");
        else
            currentState = State.Idle;
    }

}
