using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment : MonoBehaviour
{
    
    private bool isAttached = false;

    public GameObject arrowObject;
    
    public float launchAngle = 45.0f;
    private ArrowController arrowController; // Variable pour stocker la référence à ArrowController

    // Start is called before the first frame update
    void Start()
    {
        // Obtenez la référence au script ArrowController attaché à un objet dans la scène
        arrowController = FindObjectOfType<ArrowController>();
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

            // Supprimez le composant FixedJoint
            Destroy(fixedJoint);

            isAttached = false;
            
            // Désactivez la flèche lorsque la balle est attachée
            arrowObject.SetActive(false);
            
            float angleChoisi = arrowController.currentAngle;

            // Convertissez l'angle choisi en une direction dans l'espace
            float launchAngleRad = angleChoisi * Mathf.Deg2Rad;
            Vector3 launchDirection = new Vector3(Mathf.Cos(launchAngleRad), 0.0f, Mathf.Sin(launchAngleRad));

            // Choisissez la force qui convient à votre besoin
            float launchForce = 10.0f;

            // Appliquez une force au Rigidbody de la balle pour la lancer
            ballRigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
            
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isAttached && collision.gameObject.CompareTag("Balle"))
        {
            if (!isAttached && collision.gameObject.CompareTag("Balle"))
            {
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = collision.rigidbody;
                isAttached = true;

                // Activez la flèche lorsque la balle est attachée
                arrowObject.SetActive(true);

                Debug.Log("Collision");
            }
        }

       
    }
    
    public bool IsAttached()
    {
        return isAttached;
    }
}
