using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment : MonoBehaviour
{
    
    private bool isAttached = false;

    private string haveBall = null;
    
    private string joueur = null; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isAttached && collision.gameObject.CompareTag("Balle"))
        {
            FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = collision.rigidbody;
            isAttached = true;
            joueur = gameObject.name;
            Debug.Log(joueur);
            PlayerPrefs.SetString("Joueur", joueur);
        }

       
    }
}
