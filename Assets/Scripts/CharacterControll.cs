using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public GameObject Player;
    public bool canMove = true;
    private Rigidbody rbJoueur;
    private float moveSpeed = 2.0f;
    public float LimitMaxX = 0.9060148f;

    public static event System.Action<Vector3> OnPlayerMove;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public enum mouvementPlayer
    {
        Up, 
        Right, 
        Down, 
        Left, 
        None
    }

    public mouvementPlayer MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            return mouvementPlayer.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            return mouvementPlayer.Down;
           
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            return mouvementPlayer.Left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            return mouvementPlayer.Right;
        }

        return mouvementPlayer.None;
    }
 
    void Update()
    {
       
    }
  

}
