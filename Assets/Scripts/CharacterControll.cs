using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public struct CharacterControll : IMouvement
{

    public Bounds position;
    
    public IMouvement.Movement getMove(bool balle)
    {
        if (!balle)
        {
            if (Input.GetKey(KeyCode.W))
            {
                return IMouvement.Movement.Up;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                return IMouvement.Movement.Down;

            }
            else if (Input.GetKey(KeyCode.A))
            {
                return IMouvement.Movement.Left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                return IMouvement.Movement.Right;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                return IMouvement.Movement.ShootUp;

            }
            else if (Input.GetKey(KeyCode.S))
            {
                return IMouvement.Movement.ShootDown;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                return IMouvement.Movement.ShootFront;
            }

            else if (Input.GetKey(KeyCode.Space))
            {
                return IMouvement.Movement.Shoot;
            }
        }
        return IMouvement.Movement.None;
    }
 
  

}
