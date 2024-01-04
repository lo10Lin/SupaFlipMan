
using UnityEngine;

public class ContainerZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("HITTTTTT ......!!!!");
        }
        
    }


}
