using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

// Structure représentant la balle du jeu
public struct Ball
{
    public Bounds position;
    public Vector3 direction;
    
    // Constructeur de la structure Ball
    public Ball(Bounds ball)
    {
        this.position = ball;
        this.direction = Vector3.zero;
    }
}
