using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball: MonoBehaviour
{
   
    private Rigidbody rb; 
    private string player; 
    float[] PlayerSide = { 180f,-180f,135f,45f,-45f,0};
    private Vector3 lastVelocity;
    private int PointsJoueur = 0;
    private int PointsIa = 0;
    void Start()
    {
        player = PlayerPrefs.GetString("players");
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

            Debug.Log("Points pour joueur");
            PointsJoueur += 1;
            SceneManager.LoadScene("MainMenu");
        }
        else if (other.CompareTag("ButIA"))
            {
                Debug.Log("Points pour Ia");
                PointsIa += 1;
                SceneManager.LoadScene("MainMenu");
        }
         
           
        }
       
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Mur") && transform.position.z<=5.0)
        {
          
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 1);
        }
        else if (collision.gameObject.CompareTag("Mur") && transform.position.z<=12.0)
        {
            
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -1);
        }
        }
    

}
