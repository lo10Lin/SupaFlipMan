using UnityEngine;

public class GoToRoom : MonoBehaviour
{
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            CameraFollowRoom.goToRoom = true;
            //this.GetComponent<Collider>().isTrigger = false;
        }
    }
}
