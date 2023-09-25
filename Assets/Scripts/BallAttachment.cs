using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttachment : MonoBehaviour
{
    
    private bool isAttached = false;

    private GameObject spawnedArrow;
    
    private string joueur = null; 

    private float yourImpulseForce = 5f;
    
    public GameObject arrowObject;
    
    public ArrowController arrowController;

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
            
            // Obtenez la rotation actuelle de la flèche depuis ArrowController
            float currentArrowRotationZ = arrowController.GetCurrentArrowRotationZ();
            
            Debug.Log(currentArrowRotationZ);

            // Calculez la direction en utilisant la rotation actuelle de la flèche
            Vector3 arrowDirection = Quaternion.Euler(90, 90, currentArrowRotationZ) * Vector3.forward;
            
            Debug.Log(arrowDirection);

            // Appliquez une force à la balle dans la direction de la flèche
            ballRigidbody.AddForce(arrowDirection * yourImpulseForce, ForceMode.Impulse);

            // Supprimez le composant FixedJoint
            Destroy(fixedJoint);

            isAttached = false;
            
            // Détruire la flèche lors de la détachement de la balle
            if (spawnedArrow != null)
            {
                Destroy(spawnedArrow);
            }
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

            // Obtenez la direction du joueur
            Vector3 playerDirection = transform.forward; // Vous pouvez également utiliser transform.TransformDirection(Vector3.forward)

            // Définissez un offset pour le bord droit de la balle
            Vector3 offset = collision.transform.right * (0.5f); // Ajustez la valeur 0.5f selon vos besoins

            // Calculez la position de la flèche en tenant compte de l'offset
            Vector3 arrowPosition = collision.transform.position + offset;
            
            // Instantiate la flèche en tenant compte de la direction du joueur
            spawnedArrow = Instantiate(arrowObject, arrowPosition, arrowObject.transform.rotation);
            spawnedArrow.transform.parent = collision.transform; // Assurez-vous que la flèche suit la balle
            spawnedArrow.SetActive(true); // Activez la flèche

            Debug.Log("Collision");
        }

       
    }
    
    public bool IsAttached()
    {
        return isAttached;
    }
}
