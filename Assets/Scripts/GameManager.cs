using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameObject joueur;
    public GameObject ia;
    private IA.Movement move;
    private CharacterControll.mouvementPlayer movePlayer; 
    public int score = 0;
    public delegate void GameOverEvent();
    public static GameManager instance;
    private IA scripts; 
    private BallAttachment scriptsBalle;
    private CharacterControll control; 
    public bool canMove = true;
    private bool isAttached = false;
    private string player; 
    public GameObject balle;
    private Rigidbody rbIA;
    private float moveSpeed = 2.0f;
    private float smoothTime = 0.5f;
    private float delay = 1.0f; 
    private float minZ = 5.0f;
    private float maxZ = 12.0f;
    private float minX = 1.0f;
    private float maxX = 8.0f;
    private string finDePartie;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    public GameObject arrow;
    private ArrowController arrowController; 
    public float launchAngle = 45.0f;
    float[] PlayerSide = { 180f,-180f,135f,45f,-45f,0};
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        CharacterControll.OnPlayerMove += HandlePlayerMove;
    }

    private void Start()
    {
        scripts = ia.GetComponent<IA>();
        scriptsBalle = ia.GetComponent<BallAttachment>();
        control = joueur.GetComponent<CharacterControll>();
        arrow.SetActive(false);
        float rnd = Random.Range(0f, 360f); // En degrés

   
        float rndRad = rnd * Mathf.Deg2Rad;

        Vector3 direction = new Vector3(Mathf.Cos(rndRad), 0.0f, Mathf.Sin(rndRad));
        
        balle.transform.Translate(direction * moveSpeed * Time.deltaTime);
        
      
    }
    void HandlePlayerMove(Vector3 moveDirection)
    {
        moveDirection.y = 0.0f;

       
        Vector3 nouvellePosition = joueur.transform.position + (moveDirection * moveSpeed * Time.fixedDeltaTime);

        // Appliquez la nouvelle position au Rigidbody
        joueur.transform.position = nouvellePosition; 

    }
    Vector3 CalculateNewPosition(Vector3 currentPosition, Vector3 movement)
    {
        return currentPosition + (movement * moveSpeed * Time.deltaTime);
    }
    
    void Update()
    {
        ActiverMouvementIa();
        //ActiverMouvementJoueur();
        Rebond();
    }


    private void Move()
    {
        
    }
    public void Sauvegarder()
    {
        PlayerPrefs.SetFloat("PositionJoueurX", joueur.transform.position.x);
        PlayerPrefs.SetFloat("PositionJoueurY", joueur.transform.position.y);
        PlayerPrefs.SetFloat("PositionJoueurZ", joueur.transform.position.z);

        PlayerPrefs.SetFloat("PositionIAX", ia.transform.position.x);
        PlayerPrefs.SetFloat("PositionIAY", ia.transform.position.y);
        PlayerPrefs.SetFloat("PositionIAZ", ia.transform.position.z);

    

        PlayerPrefs.Save();
    }

    private void players()
    {
        
    }
     public void DetachBall()
        {
            FixedJoint fixedJoint = GetComponent<FixedJoint>();
            if (fixedJoint != null)
            {
             
                Rigidbody ballRigidbody = fixedJoint.connectedBody;
    
                Destroy(fixedJoint);
    
                isAttached = false;
                
              
                arrow.SetActive(false);
                
                float angleChoisi = arrowController.currentAngle;
    
              
                float launchAngleRad = angleChoisi * Mathf.Deg2Rad;
                Vector3 launchDirection = new Vector3(Mathf.Cos(launchAngleRad), 0.0f, Mathf.Sin(launchAngleRad));
    
              
                float launchForce = 5.0f;
    
             
                ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
                
            }
        }
     /*public void ActiverMouvementJoueur()
     {
        
         Vector3 targetPosition=Vector3.zero;
         
         movePlayer= control.MovePlayer();
         Debug.Log(movePlayer);
         switch (movePlayer)
         {
             case CharacterControll.mouvementPlayer.Up:
                 targetPosition = joueur.transform.position + Vector3.forward * moveSpeed * Time.deltaTime;
                 break;
             case CharacterControll.mouvementPlayer.Down:
                 targetPosition =  joueur.transform.position - Vector3.forward * moveSpeed* Time.deltaTime;
                 break;
             case CharacterControll.mouvementPlayer.Left:
                 targetPosition =  joueur.transform.position - Vector3.right * moveSpeed* Time.deltaTime;
                 break;
             case CharacterControll.mouvementPlayer.Right:
                 targetPosition =  joueur.transform.position + Vector3.right * moveSpeed *Time.deltaTime;
                 break;
         }
         targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
         targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
         targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
         joueur.transform.position = targetPosition;
     } */

     private void Rebond()
     {
         Vector3 currentPosition = balle.transform.position;

         if (currentPosition.z <= 5.0)
         {
             // Déplacer vers l'avant
             currentPosition.z += moveSpeed * Time.deltaTime;
             ContinueTrajectory();
         }
         else if (currentPosition.z >= 12.0)
         {
             // Déplacer vers l'arrière
             currentPosition.z -= moveSpeed * Time.deltaTime;
             ContinueTrajectory();
         }

         // Mettre à jour la position du GameObject
         balle.transform.position = currentPosition;
         
     }
     private void ContinueTrajectory()
     {
         
         Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f); 
         // Calculez le déplacement en fonction de la direction et de la vitesse
         Vector3 displacement = direction * moveSpeed * Time.deltaTime;

         // Mettez à jour la position de la balle en ajoutant le déplacement
         balle.transform.position += displacement;
     }
     private void But()
     {
         if (balle.transform.position.x>7) // Vérifie si la collision concerne le but.
         {

             Debug.Log("Points pour joueur");
            
             //Panel_Win.SetActive(true);
             //SceneManager.LoadScene("MainMenu");
         }
         else if (balle.transform.position.x<-6)
         {
             Debug.Log("Points pour Ia");
             
             //Panel_Lose.SetActive(true);
             //SceneManager.LoadScene("MainMenu");
         }
     }
    public void ActiverMouvementIa()
    {
        
         Vector3 targetPosition=Vector3.zero;
         
         move= scripts.RandomMovement();
       //Debug.Log(move);
       switch (move)
       {
           case IA.Movement.Up:
               targetPosition = ia.transform.position + Vector3.forward * moveSpeed * Time.deltaTime;
               break;
           case IA.Movement.Down:
               targetPosition =  ia.transform.position - Vector3.forward * moveSpeed* Time.deltaTime;
               break;
           case IA.Movement.Left:
               targetPosition =  ia.transform.position - Vector3.right * moveSpeed* Time.deltaTime;
               break;
           case IA.Movement.Right:
               targetPosition =  ia.transform.position + Vector3.right * moveSpeed *Time.deltaTime;
               break;
             
           case IA.Movement.shootUp:
              DetachBall();
               break;
           case IA.Movement.shootDown:
               DetachBall();
               break;
           case IA.Movement.shootFront:
               DetachBall();
               break;
       }
       targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
       targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
       targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
       ia.transform.position = targetPosition;
    } 
    public bool IsAttached()
    {
        return isAttached;
    }
}