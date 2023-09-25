using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private float[] rotationPositions = { -45.0f, 0.0f, 45.0f }; // Positions de rotation possibles
    private int currentPositionIndex = 1; // Index de la position actuelle
    public float currentAngle; // Ajoutez cette variable publique

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            // Décrémenter l'index pour aller à la position précédente
            currentPositionIndex--;
            if (currentPositionIndex < 0)
            {
                currentPositionIndex = rotationPositions.Length - 1; // Revenir à la dernière position
            }
        }

        if (Input.GetKeyDown("e"))
        {
            // Incrémenter l'index pour aller à la position suivante
            currentPositionIndex++;
            if (currentPositionIndex >= rotationPositions.Length)
            {
                currentPositionIndex = 0; // Revenir à la première position
            }
        }

        // Appliquer la rotation en fonction de la position actuelle
        float targetRotation = rotationPositions[currentPositionIndex];
        transform.localRotation = Quaternion.Euler(90, 90, targetRotation);
        
        // Mettez à jour la variable currentAngle
        currentAngle = targetRotation;
    }
}