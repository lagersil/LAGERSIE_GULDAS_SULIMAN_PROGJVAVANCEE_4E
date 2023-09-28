using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public struct IA : IMouvement
{
  
   
   
    public Bounds position; 

  
    public IMouvement.Movement getMove(bool balle)
    {
        if (!balle)
             {
                 return (IMouvement.Movement)UnityEngine.Random.Range(0, 4);
             }
             else
             {
                 return (IMouvement.Movement)UnityEngine.Random.Range(5, 8);
             }

     

    }

}