using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

// Définir la structure IA qui implémente l'interface IMouvement
public struct IA : IMouvement
{
  
   
   
    public Bounds position; 

     // Implémentation de la méthode getMove de l'interface IMouvement
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