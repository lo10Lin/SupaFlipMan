using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEvents : MonoBehaviour
{
    private List<Rigidbody> rigidbodies = new List<Rigidbody>();
    [HideInInspector] public List<EnemiesAnimEvent> enemiesAnimEvent = new List<EnemiesAnimEvent>();
    public GameObject player;

    private void Start()
    {
        foreach (EnemiesAnimEvent e in GetComponentsInChildren<EnemiesAnimEvent>())
        {
            enemiesAnimEvent.Add(e);
        }
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rigidbodies.Add(rb);
            if (rb.GetComponent<CustomGravity>().gravityDirection.y > 0)
            {
                rb.gameObject.SetActive(false);
            }
        }
    }
    void Freeze()
    {
        rigidbodies.RemoveAll(item => item == null);
        enemiesAnimEvent.RemoveAll(item => item == null);

        DestroyOnFlip[] projectiles = GetComponentsInChildren<DestroyOnFlip>();
        foreach (DestroyOnFlip p in projectiles)
        {
            Destroy(p.gameObject);
        }

        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<AttackManager>().enabled = false;
        player.GetComponentInChildren<PlayerAnimationEvents>().DisableHiboxes();
        player.GetComponentInChildren<Animator>().SetTrigger("Spin");

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        foreach (EnemiesAnimEvent e in enemiesAnimEvent)
        {
            e.DisableHitBoxes();
        }
    }

    void ChangeEnemyState()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            if (rb.GetComponent<CustomGravity>().gravityDirection.y > 0)
            {
                rb.gameObject.SetActive(true);
            }
            else if (rb.transform.parent != null)
            {   
               rb.gameObject.SetActive(false);
            }

        }
    }

    [System.Obsolete]
    void UnFreeze()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<AttackManager>().enabled = true;
        player.GetComponentInChildren<Animator>().Play("Idle");

        foreach (Rigidbody rb in rigidbodies)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            if (rb.TryGetComponent(out SpikeTrap trap) && rb.gameObject.active)
            {
                trap.Continue();
            }
                
                
            if(rb.transform.parent != null)
                rb.GetComponent<CustomGravity>().gravityDirection *= (-1);
        }
        player.GetComponentInChildren<Animator>().transform.rotation = Quaternion.identity;
    }

}
