using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaHitboxTrigger : MonoBehaviour
{
    public float deflectSpeedMultiplier = 1.1f;
    private PlayerManager manager;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("DeflectHitbox"))
        {
            other.transform.parent.gameObject.layer = LayerMask.NameToLayer("AllyProjectile");
            AudioManager.audioInstance.PlaySound(22);
            if (other.transform.parent.TryGetComponent(out ShurikenProjectile shuriken))
            {
                dir = shuriken.transform.position - manager.transform.position;
                dir.Normalize();
                shuriken.dir = new Vector3(dir.x, shuriken.dir.y, dir.z);
                shuriken.speed *= deflectSpeedMultiplier;
            }
            if (other.transform.parent.TryGetComponent(out VialProjectile vial))
            {
                vial.currentFallTime = vial.fallTime + Time.time;
                dir = vial.transform.position - manager.transform.position;
                dir.Normalize();
                vial.dir = new Vector3(dir.x, vial.dir.y, dir.z);
                vial.speed *= deflectSpeedMultiplier;
            }
            if (other.transform.parent.TryGetComponent(out KunaiProjectile kunai))
            {
                kunai.transform.rotation = Quaternion.identity; 
                dir = kunai.transform.position - manager.transform.position;
                float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                dir.Normalize();
                kunai.dir = new Vector3(dir.x, kunai.dir.y, dir.z);
                kunai.GetComponent<Transform>().Rotate(90f, targetAngle, 0f);
                kunai.speed *= deflectSpeedMultiplier;
            }
        }
        else
        {
            AudioManager.audioInstance.PlaySound(21);
        }
    }
}
