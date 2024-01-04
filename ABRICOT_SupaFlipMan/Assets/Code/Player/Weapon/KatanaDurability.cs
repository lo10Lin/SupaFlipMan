using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaDurability : MonoBehaviour
{
    public bool InfiniteLifeTime = true;
    public float LifeTime = 20f;

    private Animator playerAnimator;

    private void Start()
    {
        playerAnimator = AttackManager.instance.playerAnimationEvents.GetComponent<Animator>();
    }
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if (LifeTime <= 0)
        {
            playerAnimator.SetBool("gotWeapon", false);
            Destroy(gameObject);
        }
    }

}
