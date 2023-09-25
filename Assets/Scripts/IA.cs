using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
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
    private string winner;
    private bool win = false;

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
    public class Node
    {
        public int value;
        public List<Node> children;

        public Node(int value)
        {
            this.value = value;
            this.children = new List<Node>();
        }
    }
    //Peut servir à rien / à voir
    private List<Movement> GetNextPossibleActions()
    {
        List<Movement> possibleActions = new List<Movement>();
        
        possibleActions.Add(Movement.Up);
        possibleActions.Add(Movement.Down);
        possibleActions.Add(Movement.Left);
        possibleActions.Add(Movement.Right);

        return possibleActions;
    }
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        rbIA = GetComponent<Rigidbody>();
        //InvokeRepeating("MonteCarlo", delay, delay);
        finDePartie = PlayerPrefs.GetString("finDePartie");
        player = PlayerPrefs.GetString("Joueur");
       winner = PlayerPrefs.GetString("winner");
       // MonteCarlo();
    }

    private void MonteCarlo()
    {
        
    
    }
    private Node Selection(Node currentNode)
    {
      
    }
    private int Simulation(Node startNode, int number_simulation)
    {
      
        
    }
    private Movement GetRandomMovement()
    {
        Movement randomMovement= (Movement)Random.Range(0, System.Enum.GetValues(typeof(Movement)).Length);
        return randomMovement;
    }

    private Movement Apply(Movement movement)
    {
        switch (movement)
        {
            case movement.Up:
                targetPosition = transform.position + Vector3.forward * moveSpeed;
                break;
            case movement.Down:
                targetPosition = transform.position - Vector3.forward * moveSpeed;
                break;
            case movement.Left:
                targetPosition = transform.position - Vector3.right * moveSpeed;
                break;
            case movement.Right:
                targetPosition = transform.position + Vector3.right * moveSpeed;
                break;
        }
 
       
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
        return randomMovement; 
    }
     

    private bool won()
    {
        return (winner == "IA");
    }
    private void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.deltaTime);
    }
}