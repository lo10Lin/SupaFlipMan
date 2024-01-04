using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class CustomGravity : MonoBehaviour
{
    private Rigidbody rb;

    public float gravityScale = 10;
    public Vector3 gravityDirection = new Vector3 (0, -1, 0);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity += gravityDirection * gravityScale * Time.fixedDeltaTime;
    }
}