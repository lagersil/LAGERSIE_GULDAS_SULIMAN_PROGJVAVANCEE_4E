using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment : MonoBehaviour
{
    
  

    public float launchAngle = 45.0f;
    
    private string players; 


   
    void Start()
    {
      
       
       
    }

  
   /* void Update()
    {
        if (isAttached && players=="Joueur"){
            
            Player.GetComponent<CharacterControll>().canMove = false;
            if(Input.GetKeyDown(KeyCode.Space)){
                DetachBall();
                Player.GetComponent<CharacterControll>().canMove = true;
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isAttached && collision.gameObject.CompareTag("Balle"))
        {
            if (!isAttached && collision.gameObject.CompareTag("Balle"))
            {
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = collision.rigidbody;
                isAttached = true;

                if (gameObject.name == "Joueur")
                {
                    players = "Joueur";
                    Debug.Log(players);
                    arrowObject.SetActive(true);
                    PlayerPrefs.SetString("players", "Joueur");
                }
                if (gameObject.name == "IA")
                {
                    players = "IA";
                    arrowObject.SetActive(true);
                    PlayerPrefs.SetString("players","IA");
                }
                Debug.Log("Collision");
              
            }
        }

       
    }*/
    
    
}
