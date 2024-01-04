using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public float mCameraHeight = 15.24f;
    public Transform target;
    [SerializeField] public Vector3 offset;
    


    void LateUpdate()
    {
        Vector3 cameraPos = new Vector3(target.position.x, mCameraHeight, target.position.z);
        transform.position = cameraPos + offset;
    }
}
