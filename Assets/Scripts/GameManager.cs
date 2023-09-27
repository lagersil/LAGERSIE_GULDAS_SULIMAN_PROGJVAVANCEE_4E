using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameObject joueur;
    public GameObject ia;
    private IA.Movement move; 
    public int score = 0;

    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    public static GameManager instance;
    private IA scripts; 
    private BallAttachment scriptsBalle; 
    public bool canMove = true;

    private string player; 
    public GameObject balle;
    private Rigidbody rbIA;
    private float moveSpeed = 5.0f;
    private float smoothTime = 0.5f;
    private float delay = 1.0f; 
    private float minZ = 5.0f;
    private float maxZ = 12.0f;
    private float minX = 1.0f;
    private float maxX = 8.0f;
    private string finDePartie;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
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
    }

    private void Start()
    {
        scripts = ia.GetComponent<IA>();
        scriptsBalle = ia.GetComponent<BallAttachment>();
    }

    void Update()
    {
        ActiverMouvement();
    }
  

    public void PlayerScored(int points)
    {
        score += points;
        Debug.Log("Score : " + score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        
        if (OnGameOver != null)
        {
            OnGameOver();
        }
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
    
    
    
    public void ActiverMouvement()
    {
        
         Vector3 targetPosition=Vector3.zero;
         
         move= scripts.RandomMovement();
       Debug.Log(move);
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
               scriptsBalle.DetachBall();
               break;
           case IA.Movement.shootDown:
               scriptsBalle.DetachBall();
               break;
           case IA.Movement.shootFront:
               scriptsBalle.DetachBall();
               break;
       }
       targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
       targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
       targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
       ia.transform.position = targetPosition;
    } 
    
}