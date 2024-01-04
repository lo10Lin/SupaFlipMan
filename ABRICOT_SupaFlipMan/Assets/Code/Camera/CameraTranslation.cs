using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslation : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPos;
    public float SmoothTime = 0.01f;
    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, SmoothTime);
    }
}
