using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment2 : MonoBehaviour
{
    
    private bool isAttached = false;

    public GameObject arrowObject;
    public GameObject Player2;
    
    public float launchAngle = 45.0f;
    private ArrowController arrowController; // Variable pour stocker la référence à ArrowController
    private string players; 

    // Start is called before the first frame update
    void Awake()
    {
        arrowController = FindObjectOfType<ArrowController>();
        
    }
    void Start()
    {
      
        arrowObject.SetActive(false);
       
    }

  
    void Update()
    {
        if (isAttached && players=="Joueur2"){
            Player2.GetComponent<CharacterController2>().canMove = false;
            if(Input.GetKeyDown(KeyCode.P)){
                DetachBall();
                Player2.GetComponent<CharacterController2>().canMove = true;
            }
        }
    }

    void DetachBall()
    {
        FixedJoint fixedJoint = GetComponent<FixedJoint>();
        if (fixedJoint != null)
        {
         
            Rigidbody ballRigidbody = fixedJoint.connectedBody;

            Destroy(fixedJoint);

            isAttached = false;
            
          
            arrowObject.SetActive(false);
            
            float angleChoisi = arrowController.currentAngle + 180;

          
            float launchAngleRad = angleChoisi * Mathf.Deg2Rad;
            Vector3 launchDirection = new Vector3(Mathf.Cos(launchAngleRad), 0.0f, Mathf.Sin(launchAngleRad));

          
            float launchForce = 5.0f;

         
            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isAttached && collision.gameObject.CompareTag("Balle"))
        {
            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = collision.rigidbody;
            isAttached = true;

            if (gameObject.name == "Joueur2")
            {
                players = "Joueur2";
                Debug.Log(players);
                arrowObject.SetActive(true);
                PlayerPrefs.SetString("players", "Joueur2");
            }

            Debug.Log("Collision");

        }

    }
    
    public bool IsAttached()
    {
        return isAttached;
    }
}
