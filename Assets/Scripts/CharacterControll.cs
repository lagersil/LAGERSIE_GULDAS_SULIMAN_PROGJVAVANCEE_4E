using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public GameObject Player;
    public bool canMove = true;
    private Rigidbody rbJoueur;
    private float moveSpeed = 2.0f;
    public float LimitMaxX = 0.9060148f;

  
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
            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal = Input.GetAxis("Horizontal");
            Vector3 deplacement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            // Calculer la nouvelle position sans dépasser la limite maximale sur l'axe X
            Vector3 nouvellePosition = rbJoueur.position + (deplacement * moveSpeed * Time.fixedDeltaTime);
            nouvellePosition.x = Mathf.Clamp(nouvellePosition.x,-10, LimitMaxX);

            // Appliquer la nouvelle position au Rigidbody
            rbJoueur.MovePosition(nouvellePosition);
        }
    }

}
