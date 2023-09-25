using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Référence à l'objet balle
    public GameObject ball;

    private float[] rotationPositions = { -45.0f, 0.0f, 45.0f }; // Positions de rotation possibles
    private int currentPositionIndex = 1; // Index de la position actuelle
    
    private float currentArrowRotationZ = 0.0f;

    private void Update()
    {

        // Vérifie si la balle est attachée au joueur
        bool ballAttached = ball.activeSelf;

        // Active ou désactive l'objet flèche en fonction de l'état de la balle
        gameObject.SetActive(ballAttached);

        if (ballAttached)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Tourner la flèche à gauche (vers -45 degrés)
                currentPositionIndex = (currentPositionIndex - 1 + rotationPositions.Length) % rotationPositions.Length;
                RotateArrow(rotationPositions[currentPositionIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.E))
            {
                // Tourner la flèche à droite (vers 45 degrés)
                currentPositionIndex = (currentPositionIndex + 1) % rotationPositions.Length;
                RotateArrow(rotationPositions[currentPositionIndex]);
            }
        }
    }

    private void RotateArrow(float targetRotation)
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z = targetRotation; // Modifiez la rotation autour de l'axe Z
        currentRotation.x = 90.0f; // Fixez la rotation autour de l'axe X à 90 degrés
        currentRotation.y = 90.0f; // Fixez la rotation autour de l'axe Y à 90 degrés
        transform.rotation = Quaternion.Euler(currentRotation);
        
        // Mettez à jour la variable de rotation actuelle de l'axe Z
        currentArrowRotationZ = -targetRotation;
        
    }
    
    public float GetCurrentArrowRotationZ()
    {
        return currentArrowRotationZ;
    }
}