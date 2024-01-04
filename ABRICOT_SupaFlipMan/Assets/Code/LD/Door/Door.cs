using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] static public bool mIsOPened;
    public GameObject mDoor;

    void Update()
    {
        
        if(mIsOPened)
        {
            mDoor.SetActive(false);
            mIsOPened = false;
        }

    }
}
