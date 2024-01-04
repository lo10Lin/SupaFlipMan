using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveObject : MonoBehaviour
{
    private SphereCollider explosion;
    private Camera camera;
    private void Start()
    {
        camera = Camera.main;
        explosion = GetComponentInChildren<SphereCollider>();
    }
    public void Boom()
    {
        StartCoroutine(Explosion());
    }

    private IEnumerator Explosion()
    {
        explosion.enabled = true;
        yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        camera.SendMessage("ShakeCamera");
        Destroy(gameObject);
    }
}
