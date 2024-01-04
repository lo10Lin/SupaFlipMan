using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowRoom : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    [SerializeField] private float cameraHeight = 19.58f;
    [SerializeField] private float cameraDistanceZ = 0.09f;


    [Header("Transition duration for the camera")]
    [SerializeField] public float transitionDuration = 2.5f;
    public static bool goToRoom;


    void LateUpdate()
    {
        if(goToRoom)
        {
            StartCoroutine(CameraTransition());
            goToRoom = false;
        }
    }

    IEnumerator CameraTransition()
    {
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale/transitionDuration);

          

            Vector3 cameraPos = new Vector3(target.position.x, cameraHeight, cameraDistanceZ) + offset;
            
      
            transform.position = Vector3.Lerp(startingPos, cameraPos, t);
      
            yield return null;
        }
    }
}
