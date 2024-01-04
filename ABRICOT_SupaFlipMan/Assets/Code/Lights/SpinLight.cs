using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLight : MonoBehaviour
{

    [SerializeField] public float mSpin = 60.0f;

    void Update()
    {
        transform.Rotate(0, mSpin * Time.deltaTime, 0);
    }
}
