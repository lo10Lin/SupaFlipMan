using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{


#if false
    public string mEnemyName; //Maybe not necessary ?????
#endif
    public enum ENEMY_TYPE {E_BODY_BODY = 0, E_DISTANCE = 1, BOSS = 2};

    public ENEMY_TYPE mEnemyType = ENEMY_TYPE.E_BODY_BODY;

    public enum ENEMY_STATE { PATROL = 0, CHASE = 1, ATTACK = 2};
    public ENEMY_STATE mActiveState = ENEMY_STATE.PATROL;

    protected int mEnemyID = 0; //difficulty index

    public int mHealth = 1;

    public int mAttackDamage = 1;

    //Recovery delay in seconds after lauching an attack
    public float recoveryDelay = 1.0f;

    protected Rigidbody rigidbody;
    
    protected PlayerControl mPlayerControlRef = null;

    protected Transform mPlayerTransform = null;
    protected Transform mThisTransform = null;

    
    
    

    public float mPatrolDistance = 10.0f;


    //Total distance enemy must be from player,in Unity units, before chasing them
    //(entering chase state)
    public float mChaseDistance = 5.0f;

    //Total distance enemy must be from player before attacking them
    public float mAttackDistance = 0.1f;

    public Transform[] patrolPositions;
    private int indexCount = 0;
    private float speed = 1.5f; 

    void Update()
    {
     
    }

   
    protected virtual void Start()
    {

        mPlayerTransform = GameObject.Find("Player").GetComponent<Transform>();
        mPlayerControlRef = GameObject.Find("Player").GetComponent<PlayerControl>();

        mThisTransform = transform;

  
        ChangeState(mActiveState);
    }


    public void ChangeState(ENEMY_STATE newState)
    {

        StopAllCoroutines();


        mActiveState = newState;

        switch (mActiveState)
        {
            case ENEMY_STATE.PATROL:
                StartCoroutine(AI_Attack());
                break;
            case ENEMY_STATE.CHASE:
                StartCoroutine(AI_Chase());
                break;
            case ENEMY_STATE.ATTACK:
                StartCoroutine(AI_Patrol());
                break;
            default:
                break;
        }
    }

    protected IEnumerator AI_Patrol()
    {

        while(mActiveState == ENEMY_STATE.PATROL)
        {

            if(transform.position != patrolPositions[indexCount].position)
            {
                Vector3 nextPosition = Vector3.MoveTowards(mThisTransform.position,patrolPositions[indexCount].position,
                speed * Time.deltaTime);
                rigidbody.MovePosition(mThisTransform.position + nextPosition);

            }
            else
            {
                indexCount = (indexCount + 1) % patrolPositions.Length;
            }

#if false

            Vector3 randomPos = Random.insideUnitSphere * mPatrolDistance;

  
            randomPos += mThisTransform.position;

            transform.position = transform.position +  new Vector3(5.0f * Time.deltaTime, 0.0f, 0.0f);

#endif


            float arrivalDistnce = 10.0f;
            float timeOut = 5.0f;
            float elapsedTime = 0.0f;



            while(Vector3.Distance(mThisTransform.position, mPlayerTransform.position) > arrivalDistnce && 
                elapsedTime < timeOut)
            {
                elapsedTime += Time.deltaTime;

                if(Vector3.Distance(mThisTransform.position, mPlayerTransform.position) < mChaseDistance)
                {

                    ChangeState(ENEMY_STATE.CHASE);
                    yield break;

                }

                yield return null;
            }
        }


    }

    IEnumerator AI_Chase()
    {


        while (mActiveState == ENEMY_STATE.CHASE)
        {
          

            float distanceFromPlayer = Vector3.Distance(mThisTransform.position, mPlayerTransform.position);

            if(distanceFromPlayer < mAttackDistance)
            {
                ChangeState(ENEMY_STATE.ATTACK);
                yield break;
            }

     
            if (distanceFromPlayer > mChaseDistance)
            {
                ChangeState(ENEMY_STATE.PATROL);
                yield break;
            }

            yield return null;

        }
    }


    IEnumerator AI_Attack()
    {

        float elapsedtime = recoveryDelay;

        while (mActiveState == ENEMY_STATE.ATTACK)
        {

            elapsedtime += Time.deltaTime;


            float distanceFromPlayer = Vector3.Distance(mThisTransform.position, mPlayerTransform.position);

   
            if (distanceFromPlayer > mChaseDistance)
            {
                ChangeState(ENEMY_STATE.PATROL);
                yield break;
            }


            if (distanceFromPlayer > mAttackDistance)
            {
                ChangeState(ENEMY_STATE.CHASE);
                yield break;
            }

            if(elapsedtime >= recoveryDelay)
            {
                elapsedtime = 0;

            }

            yield return null;
        }

    }

}

