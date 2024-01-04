using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    public Transform weaponContainer;
    private SphereCollider pickUpTrigger;
    public ParticleSystem[] particles;
    private CapsuleCollider hitbox;
    private KatanaDurability katanaDurability;

    // Start is called before the first frame update
    void Start()
    {
        pickUpTrigger = GetComponent<SphereCollider>();
        particles = GetComponentsInChildren<ParticleSystem>();
        hitbox = GetComponentInChildren<CapsuleCollider>();
        katanaDurability = transform.parent.GetComponentInChildren<KatanaDurability>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponentInChildren<Animator>().SetBool("gotWeapon", true);
  
        PlayerAnimationEvents playerAnimationEvent = other. GetComponentInChildren<PlayerAnimationEvents>();
        playerAnimationEvent.KatanaHitbox = hitbox;
        playerAnimationEvent.KatanaVFX = particles[0];

        Destroy(pickUpTrigger);
        for(int i = 1; i< particles.Length; i++)
        {
            Destroy(particles[i]);
        }

        transform.SetParent(weaponContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        if(!katanaDurability.InfiniteLifeTime)
        {
            katanaDurability.enabled = true;
        }
    }
}
