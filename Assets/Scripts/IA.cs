using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
  
   
    private string player; 
    
    private float moveSpeed = 5.0f;
    private float smoothTime = 0.5f;
    private float delay = 1.0f; 
    private float minZ = 5.0f;
    private float maxZ = 12.0f;
    private float minX = 1.0f;
    private float maxX = 8.0f;
    
    private bool haveBall = false;
    
    
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
        
        InvokeRepeating("RandomMovement", delay, delay); ;
        player = PlayerPrefs.GetString("players");
     
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
        Mouv(); 
             if (!haveBall)
             {
                 return (Movement)UnityEngine.Random.Range(0, 4);
             }
             else
             {
                 return (Movement)UnityEngine.Random.Range(5, 8);
             }
        
     }

}