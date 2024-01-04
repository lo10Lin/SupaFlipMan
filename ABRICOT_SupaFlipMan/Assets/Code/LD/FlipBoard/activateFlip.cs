using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateFlip : MonoBehaviour
{
    public bool isReverse;
    private Animator flipAnim;
    private void Awake()
    {
        flipAnim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Rigidbody>().useGravity = false;
        AudioManager.audioInstance.PlaySound(5);
        if (isReverse)
        {

            flipAnim.SetTrigger("isReverse");
        }
        else
        {
            flipAnim.SetTrigger("isInitPlace");
        }
    }
}
