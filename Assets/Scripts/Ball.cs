using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball: MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb; 
    private string player; 
    float[] PlayerSide = { 45f,-45f,0f,180f,-180f,135f};
    private Vector3 lastVelocity;
    private int PointsJoueur = 0;
    private int PointsIa = 0;
    void Start()
    {
        player = PlayerPrefs.GetString("Joueur");
        Debug.Log(player);
        rb = GetComponent<Rigidbody>();
        float rnd = PlayerSide[Random.Range(0, PlayerSide.Length)];
        Vector3 direction = new Vector3(Mathf.Cos(rnd), 0.0f, Mathf.Sin(rnd));
        rb.AddForce(direction * 5f, ForceMode.Impulse);
        Debug.Log("Le nombre aléatoire est : " + rnd);
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("But")) // Vérifie si la collision concerne le but.
        {
            if (player == "Joueur")
            {
                Debug.Log("Points pour joueur");
                PointsJoueur += 1;
            }
            else
            {
                Debug.Log("Points pour Ia");
                PointsIa += 1;
            }
         
            // Vous pouvez ajouter ici le code pour gérer le succès ou tout autre comportement souhaité.
        }
       
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mur") && transform.position.z<=5.0)
        {
            // Inversez la composante Z de la vélocité pour simuler le rebond, tout en gardant X et Y inchangés.
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 1);
        }
        else if (collision.gameObject.CompareTag("Mur") && transform.position.z<=12.0)
        {
            // Inversez la composante Z de la vélocité pour simuler le rebond, tout en gardant X et Y inchangés.
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -1);
        }
        }
    
 
}
