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
    private enum Movement
    {
        Up,
        Down,
        Left,
        Right
    };

    private enum Shoot
    {
        Up,
        Down,
        Front
    };

    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        rbIA = GetComponent<Rigidbody>();
        InvokeRepeating("RandomMovement", delay, delay);
        finDePartie = PlayerPrefs.GetString("finDePartie");
        player = PlayerPrefs.GetString("Joueur");
       // MonteCarlo();
    }

   
    private void RandomMovement()
     {
       
         Movement randomMovement = (Movement)Random.Range(0, System.Enum.GetValues(typeof(Movement)).Length);
 
       
         switch (randomMovement)
         {
             case Movement.Up:
                 targetPosition = transform.position + Vector3.forward * moveSpeed;
                 break;
             case Movement.Down:
                 targetPosition = transform.position - Vector3.forward * moveSpeed;
                 break;
             case Movement.Left:
                 targetPosition = transform.position - Vector3.right * moveSpeed;
                 break;
             case Movement.Right:
                 targetPosition = transform.position + Vector3.right * moveSpeed;
                 break;
         }
 
       
         targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
         targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
         targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
     }

    private void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
        }
    }
}