using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake2 : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField] private Transform orignalCameraPos;
   


    [SerializeField] private float shakeDuration = 1.0f;
    [SerializeField] private float shakeAmount = 0.1f;

    [SerializeField] private bool camIshaking = false;
    [SerializeField] public bool makeCamShake = false;

    [SerializeField] private float shakeTimer;





    void Start()
    {
        cameraTransform = GetComponent<Transform>();
        
    }


    void Update()
    {
        if (camIshaking)
        {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera()
    {
        camIshaking = true;
        shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (shakeTimer > 0)
        {
            cameraTransform.localPosition = orignalCameraPos.position + Random.insideUnitSphere * shakeAmount;


            shakeTimer -= Time.deltaTime;
        }
        else
        {
            shakeTimer = 0f;
            cameraTransform.position = orignalCameraPos.position;
            camIshaking = false;
        }
    }
}
