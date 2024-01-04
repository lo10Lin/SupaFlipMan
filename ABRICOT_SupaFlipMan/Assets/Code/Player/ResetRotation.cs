using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotation : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.localRotation = Quaternion.identity;
        enabled = false;
    }
}
