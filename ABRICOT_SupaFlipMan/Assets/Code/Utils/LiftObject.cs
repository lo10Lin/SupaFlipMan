using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftObject : MonoBehaviour
{
    private Transform boardTransform;
    public LayerMask groundLayer;
    private Rigidbody rb;
    private PhysicMaterial pm;
    private float OriginalStaticFriction;
    private float OriginalDynamicFriction;

    public float liftForce = 15f;
    private float rayDistance;
    public bool isGrounded = false;
    public float isGroundedOffset = 0.01f;

    private void Start()
    {
        boardTransform = transform.parent;
        rb = GetComponent<Rigidbody>();
        rayDistance = GetComponent<Collider>().bounds.size.y/2f + isGroundedOffset;
        pm = GetComponent<Collider>().material;
        OriginalStaticFriction = pm.staticFriction;
        OriginalDynamicFriction = pm.dynamicFriction;

    }

    private void FixedUpdate()
    {
        if (!isGrounded && Physics.Raycast(transform.position, Vector3.down, out _, rayDistance, groundLayer.value))
        {
            AudioManager.audioInstance.PlaySound(46);
            pm.bounciness = 1;
            pm.staticFriction = OriginalStaticFriction;
            pm.dynamicFriction = OriginalDynamicFriction;
            isGrounded = true;
            transform.parent = boardTransform;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    public void Lift()
    {
        print(rayDistance);
        if (isGrounded)
        {
            pm.bounciness = 0;
            pm.dynamicFriction = 0;
            pm.staticFriction = 0;
            isGrounded = false;
            rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            rb.AddForce(Vector3.up * liftForce, ForceMode.VelocityChange);
            transform.parent = null;
        }
    }
}
