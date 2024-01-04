using UnityEngine;

public class MovingBoxUp : MonoBehaviour
{

    [SerializeField] private bool mCanBeBumped;
    [SerializeField] private bool mIsGrounded;
    [SerializeField] public float rayDistance = 1f;
    [SerializeField] private LayerMask mLayerMask;

    [SerializeField] [Range(0.0f, 50.0f)] public float mJumpForce = 6.0f;

    public Transform roomTransform;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDistance, Color.red);
        if (!mIsGrounded && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out _, rayDistance, mLayerMask))
        {
            mIsGrounded = true;
            mCanBeBumped = true;
            transform.parent = roomTransform;
        }
    }

    private void OnCollisionStay(Collision other) 
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.GetComponentInChildren<Animator>().GetBool("isSliding") && !other.gameObject.GetComponentInChildren<Animator>().GetBool("isDashAttacking") && mCanBeBumped)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
            rb.AddForce(Vector3.up * mJumpForce, ForceMode.VelocityChange);
            transform.parent = null;
            mIsGrounded = false;
            mCanBeBumped = false;
        }
    }
}

