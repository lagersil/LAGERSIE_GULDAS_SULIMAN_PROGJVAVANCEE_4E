using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private float[] rotationPositions = { -45.0f, 0.0f, 45.0f };
    private float[] rotationPositionsIa = { -180f, 135f, 180f };
    private int currentPositionIndex = 1; 
    public float currentAngle;
    private string Player;


    
    void Update()
    {

        Player = PlayerPrefs.GetString("players");
        Debug.Log(Player + "eeeee");
        if (Player == "Joueur")
        {
            if (Input.GetKeyDown("q"))
            {

                currentPositionIndex--;
                if (currentPositionIndex < 0)
                {
                    currentPositionIndex = rotationPositions.Length - 1;
                }
            }

            if (Input.GetKeyDown("e"))
            {
                currentPositionIndex++;
                if (currentPositionIndex >= rotationPositions.Length)
                {
                    currentPositionIndex = 0;
                }
            }


            float targetRotation = rotationPositions[currentPositionIndex];
            transform.localRotation = Quaternion.Euler(90, 90, targetRotation);


            currentAngle = targetRotation;

        }

        if (Player == "IA")
        {
           
            int rnd = Random.Range(0, rotationPositionsIa.Length);
            
            float targetRotationIa = rotationPositionsIa[rnd];
            
            transform.localRotation = Quaternion.Euler(90, 90, targetRotationIa);
            
            currentAngle = targetRotationIa;
        }
    }
}