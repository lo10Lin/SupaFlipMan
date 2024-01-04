using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] public float mSpeed = 10.0f;
    private Vector3 mDirection;

    [SerializeField] public float mJumpForce = 25.0f;
    [SerializeField] private bool mIsJumping;


    [SerializeField] bool mIsGrounded;
    [SerializeField] private LayerMask mLayerMask;
    [SerializeField] private LayerMask mButtomLayerMask;
    [SerializeField] private LayerMask mDashingLayerMask;

    private float rayDistance = 1.1f;
    private float rayDashingDistance = 0.6f;



    [SerializeField] private bool mIsDashing;
    [SerializeField] public bool mStayInDash;

    [SerializeField] private bool mDoorOpened;
    private PlayerControl playerControlRef;

    public bool IsDashing
    {
        get
        {
            return mStayInDash;
        }
    }



    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerControlRef = GameObject.Find("Player").GetComponent<PlayerControl>();

    }

   
    void Update()
    {
        mDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(Input.GetKeyDown(KeyCode.B))
           playerControlRef.movingBox = true;
        else if(Input.GetKeyUp(KeyCode.B))
            playerControlRef.movingBox = false;



        if (Input.GetKeyDown(KeyCode.X))
        {
            mIsDashing = true;
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {

            transform.eulerAngles = new Vector3(0, 0, 0);
            mIsDashing = false;

        }


        if (Input.GetKeyDown(KeyCode.Space))
            mIsJumping = true;


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

        //Buttom to open door
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDistance, mButtomLayerMask))
        {
            Door.mIsOPened = true;
        }
        else
        {
            Door.mIsOPened = false;
        }




        //For dash
#if true
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * rayDashingDistance, Color.red);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * rayDashingDistance, Color.red);
#endif
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, rayDashingDistance, mDashingLayerMask)
        || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, rayDashingDistance, mDashingLayerMask))
        {
            mStayInDash = true;
        }
        else
        {
            mStayInDash = false;
        }



        if (mIsJumping)
            OnJump();


        if (mIsDashing)
            OnDash();

        MovePlayer(mDirection);

    }

    void MovePlayer(Vector3 direction)
    {

#if false

        rigidbody.MovePosition(transform.position + direction * mSpeed * Time.deltaTime);
#endif

#if true



        Vector3 playerVelocity = rigidbody.velocity;

        playerVelocity.x = mDirection.x * mSpeed;
        playerVelocity.y = rigidbody.velocity.y;
        playerVelocity.z = mDirection.z * mSpeed;

        rigidbody.velocity = playerVelocity;


#endif

    }

    void OnJump()
    {
        if (mIsJumping && mIsGrounded)
        {
            rigidbody.AddForce(Vector3.up * mJumpForce, ForceMode.Impulse);
            mIsJumping = false;
        }
    }


    void OnDash()
    {
        if (mIsDashing && !mIsGrounded)
            return;

        if(mIsDashing && mIsGrounded)
        {
            float angle = mDirection.x >=0 ? 90.0f : -90.0f;

            Vector3 newRotation = new Vector3(0, 0, angle);

            transform.eulerAngles = newRotation;

            mIsDashing = false;

        }
    }

}
