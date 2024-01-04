using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    private void Awake()
    {
        instance = this;
    }

    public event Action addPlayerLife;
    public void addLife()
    {
        addPlayerLife?.Invoke(); 
    }

    public event Action<float> removePlayerLife;
    public void takeDamage(float damage)
    {
        removePlayerLife?.Invoke(damage);
    }
}
