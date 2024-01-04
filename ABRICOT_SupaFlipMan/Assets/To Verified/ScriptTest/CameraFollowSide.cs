using UnityEngine;

public class CameraFollowSide : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] [Range(0.0f, 1.0f)]
    private float smoothSpeed = 0.0f;

    private Vector3 velocity = Vector3.zero;


    void LateUpdate()
    {
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position,desiredPos,ref velocity, smoothSpeed);
        
    }
}
