using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject weaponRef;
    private GameObject project;

    [SerializeField] private float projectileCoolDown = 1.0f;
    
    [SerializeField][Range(0.0f, 1000.0f)] private float projectileSpeed = 200.0f;
    [SerializeField]private float mDamage = 1.0f;
    private float angle;
    private float zRotation = 90.0f;
    private float yRotation = 90.0f;

    private float yOffsetRotation = 180.0f; // ?????? 
    private float mCoolDown = 0.0f;

    public ParticleSystem canonVFX;

    void Start()
    {
        project = new GameObject();
    }

    void Update()
    {
        angle = Mathf.Atan2(transform.position.x,transform.position.z) * Mathf.Rad2Deg;

        mCoolDown -= Time.deltaTime;

        if(mCoolDown <= 0.0f)
        {
            Instantiate(canonVFX, transform.position, Quaternion.identity);
            AudioManager.audioInstance.PlaySound(34);
            project = Instantiate(weaponRef,transform.position,transform.rotation);
            project.transform.eulerAngles += new Vector3(0.0f, yRotation, zRotation);
            project.GetComponent<KunaiProjectile>().InitKunai(transform.forward,projectileSpeed,mDamage, 0.0f);

            mCoolDown = projectileCoolDown;
        }

    }

}
