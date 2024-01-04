using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private float mTimeForStop = 0.2f;
    private float mMoveSpeed = 20f;
    [HideInInspector] public bool isMoving = false;

    private Rigidbody rb;
    private Vector3 horizontalRotation = new Vector3(0, 90, 90);
    private Vector3 verticalRotation = new Vector3(0, 0, 90);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void MoveBarrel(Vector3 direction)
    {
        isMoving = true;
        StopAllCoroutines();
        rb.isKinematic = false;
        if (Vector3.Dot(direction, Vector3.right) >= 0.71f)
        {
            direction = Vector3.right;
            transform.eulerAngles = horizontalRotation;
        }
        else if (Vector3.Dot(direction, Vector3.left) >= 0.71f)
        {
            direction = Vector3.left;
            transform.eulerAngles = horizontalRotation;
        }
        else if (Vector3.Dot(direction, new Vector3(0, 0, 1)) > 0)
        {
            direction = new Vector3(0, 0, 1);
            transform.eulerAngles = verticalRotation;
        }
        else
        {
            direction = new Vector3(0, 0, -1);
            transform.eulerAngles = verticalRotation;
        }
        rb.AddForce(direction.normalized * mMoveSpeed, ForceMode.VelocityChange);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Player") && other.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(StopBarrel());
        }
    }

    private IEnumerator StopBarrel()
    {
        yield return new WaitForSeconds(mTimeForStop);
        isMoving = false;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
}
