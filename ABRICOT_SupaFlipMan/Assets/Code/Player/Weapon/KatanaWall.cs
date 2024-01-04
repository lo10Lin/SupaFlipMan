using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaWall : MonoBehaviour
{
    private Camera camera;


    void Start()
    {
        camera = Camera.main;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            AudioManager.audioInstance.PlaySound(28);
            Destroy(other.gameObject);
            camera.SendMessage("ShakeCamera");

        }
    }
}
