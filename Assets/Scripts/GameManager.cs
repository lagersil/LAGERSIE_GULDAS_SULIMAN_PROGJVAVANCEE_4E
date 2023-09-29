using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

//Gere la logique de jeu de l'application.
public class GameManager : MonoBehaviour
{
    
    public GameObject joueurGO;
    public GameObject iaGO;
    public static GameManager instance;
    public GameObject balleGO;
    private string finDePartie;
    private GameState game;
    float[] possibleAngles = { 45f, -45f, 180f, 135f, -135f };
    private int iteration = 30;
    private GameObject GO; 
    private GameObject MCTS;
    public GameObject PanelLose;
    public GameObject PanelWin;
    private ChoicePlayer joueur2;

    // Methode appelee au demarrage du jeu
    private void Start()
    { 
        game = new GameState();
        game.joueur = new CharacterControll();
        game.mcts = new MCTS(iteration);
        game.balle = new Ball();
        game.humain2 = new Joueur2();
        game.choix = new ChoicePlayer();

        
        float randomAngle = possibleAngles[Random.Range(0, possibleAngles.Length)];
    
        float randomAngleRad = randomAngle * Mathf.Deg2Rad;
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngleRad), 0f, 0f);
        if (game.ChoixJoueur2() == "MCTS")
        {
            game.mcts.position.center = iaGO.transform.position;
            game.mcts.position.size = iaGO.transform.lossyScale;
        }
        else if (game.ChoixJoueur2() == "Humain")
        {
            game.humain2.position.center = iaGO.transform.position;
            game.humain2.position.size = iaGO.transform.lossyScale;
        }
        else if (game.ChoixJoueur2() == "IA")
        {
            game.ia.position.center = iaGO.transform.position;
            game.ia.position.size = iaGO.transform.lossyScale;
        }
        game.balle.direction = randomDirection * 25.0f * Time.deltaTime;
        game.joueur.position.center = joueurGO.transform.position;
       
        game.balle.position.center = balleGO.transform.position;
        game.joueur.position.size = joueurGO.transform.lossyScale;
       
        game.balle.position.size = balleGO.transform.lossyScale;

    }

    // Methode pour gerer le deplacement des joueurs et de la balle
    void HandlePlayerMove()
    {
  
        if (game.ChoixJoueur2() == "MCTS")
        {
            iaGO.transform.position = game.mcts.position.center;
        }
        else if (game.ChoixJoueur2() == "Humain")
        {
            iaGO.transform.position = game.humain2.position.center;
        }
        else if (game.ChoixJoueur2() == "IA")
        {
            iaGO.transform.position = game.ia.position.center;
        }
        joueurGO.transform.position = game.joueur.position.center;
        balleGO.transform.position = game.balle.position.center;
    }
    
    // Methode pour afficher les panneaux de victoire et de defaite
    void WinLosePanel()
    {
        if (game.victoireIA)
        {
            PanelLose.SetActive(true);
            StartCoroutine(LoadMainMenuAfterDelay());
            
        }
        else if (game.victoireJoueur)
        {
            PanelWin.SetActive(true);
            StartCoroutine(LoadMainMenuAfterDelay());
        }

       
    }

    // Coroutine pour charger le menu principal apres un delai
    private IEnumerator LoadMainMenuAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        // Charger la scène "MainMenu"
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        
        if (game.ChoixJoueur2() == "MCTS")
        {
            game.Tick(Time.deltaTime,game.joueur.getMove(game.PlayerHaveBall),game.mcts.MonteCarlo(game)); 
        }
        else if (game.ChoixJoueur2() == "Humain")
        {
            game.Tick(Time.deltaTime,game.joueur.getMove(game.PlayerHaveBall),game.humain2.getMove(game.IaHaveBall)); 
        }
        else if (game.ChoixJoueur2() == "IA")
        {
            game.Tick(Time.deltaTime,game.joueur.getMove(game.PlayerHaveBall),game.ia.getMove(game.IaHaveBall)); 
        }
        HandlePlayerMove();
        WinLosePanel();
    }
  

    
}