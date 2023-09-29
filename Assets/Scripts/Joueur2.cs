using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

// Définir la structure Joueur2 qui implémente l'interface IMouvement
public struct Joueur2 : IMouvement
{

    public Bounds position;
    
    // Implémentation de la méthode getMove de l'interface IMouvement
    public IMouvement.Movement getMove(bool balle)
    {
        if (!balle)
        {
            if (Input.GetKey(KeyCode.U))
            {
                return IMouvement.Movement.Up;
            }
            else if (Input.GetKey(KeyCode.J))
            {
                return IMouvement.Movement.Down;

            }
            else if (Input.GetKey(KeyCode.H))
            {
                return IMouvement.Movement.Left;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                return IMouvement.Movement.Right;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.U))
            {
                return IMouvement.Movement.ShootUp;

            }
            else if (Input.GetKey(KeyCode.J))
            {
                return IMouvement.Movement.ShootDown;
            }
            else if (Input.GetKey(KeyCode.H))
            {
                return IMouvement.Movement.ShootFront;
            }

            else if (Input.GetKey(KeyCode.P))
            {
                return IMouvement.Movement.Shoot;
            }
        }
        return IMouvement.Movement.None;
    }
 
  

}