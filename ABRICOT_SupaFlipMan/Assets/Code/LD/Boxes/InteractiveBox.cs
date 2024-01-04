using UnityEngine;

public class InteractiveBox : MonoBehaviour
{

    [SerializeField] private bool mIsMovingUp;
    [HideInInspector] public bool mIsHittedbyPlayer;

    [SerializeField] private bool mIsGrounded;
    [SerializeField] public float rayDistance = 0.5f;

    [SerializeField] private LayerMask mLayerMask;


    [SerializeField] [Range(0.0f, 100.0f)] public float mJumpForce = 20.0f;
    [SerializeField] [Range(0.0f, 200.0f)] private float mPowerForce = 70.0f;
    [SerializeField] [Range(0.0f, 200.0f)] private float mWallForce = 10.0f;
    [SerializeField] [Range(0.0f, 200.0f)] private float mWallForceToPlayer = 20.0f;



    [SerializeField] private Animator playerAnimatorRef;
    [SerializeField]private Rigidbody mPlayerRigidbodyRef;
  


    private Rigidbody rigidbody;





    void Start()
    {

       rigidbody = GetComponent<Rigidbody>();

    }

    void Update()
    {

    }


    private void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, mLayerMask))
        {
            mIsGrounded = true;
        }
        else
        {
            mIsGrounded = false;
        }

        if(mIsMovingUp)
        {
            rigidbody.isKinematic = false;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;

            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            MoveBoxUp();

        }
        

        if(!mIsGrounded)
        {
            rigidbody.isKinematic = false;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
            rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;

            rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        }


    }


    void MoveBoxUp()
    {
        if(mIsGrounded && mIsMovingUp)
        {
            rigidbody.AddForce(Vector3.up * mJumpForce, ForceMode.Impulse);

            mIsMovingUp = false;
        }

    }

    void HittedbyPlayer(Vector3 direction)
    {
        mIsHittedbyPlayer = true;

        if(mIsHittedbyPlayer)
        {
            rigidbody.AddForce(direction * mPowerForce, ForceMode.Impulse);
        }

        mIsHittedbyPlayer = false;
    }

    private void OnCollisionEnter(Collision other) 
    {


        if(other.gameObject.tag == "Player" && playerAnimatorRef.GetBool("isSliding"))
        {
            mIsMovingUp = true;
            rigidbody.isKinematic = false;

        }
        else if(other.gameObject.tag == "Enemy")
        {
            rigidbody.isKinematic = true;

        }


        if(other.gameObject.tag == "Wall")
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(-other.gameObject.transform.position * mWallForce,ForceMode.Impulse);
            mPlayerRigidbodyRef.AddForce(-rigidbody.position * mWallForceToPlayer,ForceMode.Impulse);

        }
 
    }

#if true
    void OnCollisionStay(Collision other)
    {
        
        if(other.gameObject.tag == "Wall")
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(-other.gameObject.transform.position * 100,ForceMode.Impulse);
        }
    }

#endif 
    private void OnCollisionExit(Collision other) 
    {
        if(other.gameObject.tag == "Player" )
        {
            mIsMovingUp = false;
        }
        else if(other.gameObject.tag == "Enemy")
        {
            rigidbody.isKinematic = false;

        }
        
    }

}
