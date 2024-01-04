using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public float pushForce = 200f;
    public float bumpForce = 10f;
    private Rigidbody rb;
    private LiftObject liftObject;
    private readonly float dotProductMinimum = 0.7f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        liftObject = GetComponent<LiftObject>();
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            if (liftObject.isGrounded && Vector3.Dot((transform.position - player.transform.position).normalized, player.direction) > dotProductMinimum)
            {
                rb.velocity = player.direction * Time.deltaTime * pushForce;
            }
        }
    }
    
    public void BumpBox(Vector3 playerPos)
    {
        Vector3 direction = new Vector3(transform.position.x - playerPos.x, 0, transform.position.z - playerPos.z).normalized;
        rb.AddForce(direction * bumpForce, ForceMode.VelocityChange);
    }
}
