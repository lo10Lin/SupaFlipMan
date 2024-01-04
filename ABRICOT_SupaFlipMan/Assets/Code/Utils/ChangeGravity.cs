using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour
{
    public Transform board;
    public bool TopZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<CustomGravity>(out CustomGravity gravity))
        {
            gravity.gravityDirection *= (-1);
        }
    }
}
