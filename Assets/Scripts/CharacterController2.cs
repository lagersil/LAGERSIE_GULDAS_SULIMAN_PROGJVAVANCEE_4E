using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    public GameObject Player;
    public bool canMove = true;
    private Rigidbody rbJoueur;
    private float moveSpeed = 2.0f;
    public float LimitMinX = 0.9060148f;

  
    // Start is called before the first frame update
    void Start()
    {
        rbJoueur = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            float moveVertical = 0f;
            float moveHorizontal = 0f;
            
            if (Input.GetKey("i")) // Touche alternative pour aller vers le haut.
            {
                moveVertical = 1f;
            }
            else if (Input.GetKey("k")) // Touche alternative pour aller vers le bas.
            {
                moveVertical = -1f;
            }

            if (Input.GetKey("j")) // Touche alternative pour aller à gauche.
            {
                moveHorizontal = -1f;
            }
            else if (Input.GetKey("l")) // Touche alternative pour aller à droite.
            {
                moveHorizontal = 1f;
            }
            
            Vector3 deplacement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            // Calculer la nouvelle position sans dépasser la limite maximale sur l'axe X
            Vector3 nouvellePosition = rbJoueur.position + (deplacement * moveSpeed * Time.fixedDeltaTime);
            nouvellePosition.x = Mathf.Clamp(nouvellePosition.x,LimitMinX, 10);

            // Appliquer la nouvelle position au Rigidbody
            rbJoueur.MovePosition(nouvellePosition);
        }
    }

}