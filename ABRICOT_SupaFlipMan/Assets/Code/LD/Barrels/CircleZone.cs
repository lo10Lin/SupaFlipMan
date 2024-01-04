using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleZone : MonoBehaviour
{
    private float rotation = 90.0f;
    private Barrel barrel;

    private void Start()
    {
        barrel = transform.parent.GetComponent<Barrel>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && !barrel.isMoving)
        {
            transform.parent.transform.eulerAngles += new Vector3(0,rotation,0);
        }
    }
}
