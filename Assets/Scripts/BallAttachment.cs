using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment : MonoBehaviour
{
    
    private bool isAttached = false;


    
    private string joueur = null; 

    private float yourImpulseForce = 5f;
    
    public GameObject arrowObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached && Input.GetKeyDown(KeyCode.Space))
        {
            DetachBall();
        }
    }
    
    void DetachBall()
    {
        FixedJoint fixedJoint = GetComponent<FixedJoint>();
        if (fixedJoint != null)
        {
            // Récupérez le Rigidbody de la balle
            Rigidbody ballRigidbody = fixedJoint.connectedBody;

            // Appliquez une impulsion à la balle pour la détacher
            ballRigidbody.AddForce(Vector3.forward * yourImpulseForce, ForceMode.Impulse);

            // Supprimez le composant FixedJoint
            Destroy(fixedJoint);

            isAttached = false;
            
           
        }
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
 
            // Activez la flèche lorsque la balle est attachée
            arrowObject.SetActive(true);
            
            Debug.Log("Collision");

        }

       
    }
    
    public bool IsAttached()
    {
        return isAttached;
    }
}
