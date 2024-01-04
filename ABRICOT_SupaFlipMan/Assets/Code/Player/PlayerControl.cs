using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private GameData gameDataRef;
    [HideInInspector] public bool movingBox;
    [HideInInspector] public Transform playerTransform;
 
    
    void Start()
    {
        gameDataRef = GameObject.Find("GameManager").GetComponent<GameData>();
        playerTransform = transform;
      
    }

 
    void Update()
    {


        if(gameDataRef.playerLives <= 0)
            gameDataRef.ResetPlayer();



    }

#if false
    void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 25), string.Format("Score: " + gameDataRef.playerScore));

        GUI.Box(new Rect(Screen.width - 110, 10, 100, 25), string.Format("Lives left: " + gameDataRef.playerLives));

    }
#endif


    void OnCollisionEnter(Collision collision)
    {

#if false
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(transform.position.y >= collision.transform.position.y + collision.transform.localScale.y/ 2)
            {

                gameDataRef.playerScore += (int)collision.gameObject.transform.localScale.x * 10;
                Destroy(collision.gameObject);
            }
            else
            {
                gameDataRef.playerLives -= 1;
            }
            
        }        
#endif


#if true
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(transform.position.y >= collision.transform.position.y + collision.transform.localScale.y/ 2)
            {
                this.SendMessage("HealDamage",250 * Time.deltaTime);
                //gameDataRef.playerScore += (int)collision.gameObject.transform.localScale.x * 10;
                Destroy(collision.gameObject);
            }
            else
            {
                //gameDataRef.playerLives -= 1;
                this.SendMessage("TakeDamage",250 * Time.deltaTime);

            }
        }

#endif

    }




}
