using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public GameObject Player;
    public bool canMove = true;
    private Rigidbody rbJoueur;
    private float moveSpeed = 5.0f;
    public float LimitMaxX = 0.5060148f;

  
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
            transform.Translate(deplacement * moveSpeed * Time.deltaTime);
        }
    }

}
