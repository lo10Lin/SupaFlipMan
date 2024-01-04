using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
 
    static public bool mEnemyAttackZone;
    
    [SerializeField] private bool mAttack;

    [SerializeField] public float mSpeed = 5.0f;


    public Transform targetTransform;

    [SerializeField] public float mattackRange = 5.0f;




   


    void Start()
    {
        
    }


    void Update()
    {
        Vector3 toTarget = targetTransform.position - transform.position;
        float distanceToTarget = toTarget.magnitude;

        float speed = mSpeed * Time.deltaTime;


        if (speed >= distanceToTarget)
        {
            //Hit the player
        }
        else
        {
            if(toTarget.magnitude <= mattackRange)
                transform.position += (toTarget / distanceToTarget) * speed;
        }

    }

}
