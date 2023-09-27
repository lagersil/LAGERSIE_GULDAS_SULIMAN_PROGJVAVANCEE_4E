using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    public bool canMove = true;
    public GameObject IAPlayer;
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
    private bool haveBall = false;
    
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;
    
    public enum Movement
    {
        Up,
        Down,
        Left,
        Right,
        shootUp, 
        shootDown,
        shootFront
    };

    private void Start()
    {
        rbIA = GetComponent<Rigidbody>();
        InvokeRepeating("RandomMovement", delay, delay);
        finDePartie = PlayerPrefs.GetString("finDePartie");
        player = PlayerPrefs.GetString("players");
       // MonteCarlo();
    }

    private void Mouv()
    {
        if (player == "IA")
        {
            haveBall = true;
        }
        else
        {
            haveBall = false;
        }
    }
   
    public Movement RandomMovement()
     {
       
         if (!haveBall)
         {
             return (Movement)UnityEngine.Random.Range(0,3);
         }
         else
         {
             return (Movement)UnityEngine.Random.Range(4,6);
         }
     }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
        }
    }
}