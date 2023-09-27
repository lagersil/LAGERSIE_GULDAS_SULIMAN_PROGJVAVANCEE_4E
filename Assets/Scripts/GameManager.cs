using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject joueur;
    public GameObject ia;

    public int score = 0;

    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }
    
    private void Update()
    {
        
    }

    public void PlayerScored(int points)
    {
        score += points;
        Debug.Log("Score : " + score);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }
    
    
    
    public void Sauvegarder()
    {
        PlayerPrefs.SetFloat("PositionJoueurX", joueur.transform.position.x);
        PlayerPrefs.SetFloat("PositionJoueurY", joueur.transform.position.y);
        PlayerPrefs.SetFloat("PositionJoueurZ", joueur.transform.position.z);

        PlayerPrefs.SetFloat("PositionIAX", ia.transform.position.x);
        PlayerPrefs.SetFloat("PositionIAY", ia.transform.position.y);
        PlayerPrefs.SetFloat("PositionIAZ", ia.transform.position.z);

        PlayerPrefs.SetInt("JoueurPossedeLaBalle", joueurPossedeLaBalle ? 1 : 0);

        PlayerPrefs.Save();
    }
    
    
    
    public void ActiverMouvement()
    {
        
        switch (move)
        {
            case move.Up:
                targetPosition = transform.position + Vector3.forward * moveSpeed;
                break;
            case move.Down:
                targetPosition = transform.position - Vector3.forward * moveSpeed;
                break;
            case move.Left:
                targetPosition = transform.position - Vector3.right * moveSpeed;
                break;
            case move.Right:
                targetPosition = transform.position + Vector3.right * moveSpeed;
                break;
        }
    }    
}