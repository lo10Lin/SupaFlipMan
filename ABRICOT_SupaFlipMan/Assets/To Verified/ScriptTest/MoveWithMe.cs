using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        other.transform.parent = null;
    }
}
