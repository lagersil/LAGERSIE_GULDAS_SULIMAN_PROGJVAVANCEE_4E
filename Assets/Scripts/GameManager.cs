using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

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
    private void Start()
    { 
        game = new GameState();
        game.joueur = new CharacterControll();
        game.mcts = new MCTS(iteration);
        game.balle = new Ball();
        
        float randomAngle = possibleAngles[Random.Range(0, possibleAngles.Length)];
    
        float randomAngleRad = randomAngle * Mathf.Deg2Rad;
        Vector3 randomDirection = new Vector3(Mathf.Cos(randomAngleRad), 0f, 0f);
        
        game.balle.direction = randomDirection * 5.0f * Time.deltaTime;
        game.joueur.position.center = joueurGO.transform.position;
        game.ia.position.center = iaGO.transform.position;
        game.balle.position.center = balleGO.transform.position;
        game.joueur.position.size = joueurGO.transform.lossyScale;
        game.ia.position.size = iaGO.transform.lossyScale;
        game.balle.position.size = balleGO.transform.lossyScale;
      
   

    }
    void HandlePlayerMove()
    {
  
        joueurGO.transform.position = game.joueur.position.center;
        iaGO.transform.position = game.ia.position.center;
        balleGO.transform.position = game.balle.position.center;
    }
 
    void Update()
    {
        game.Tick(Time.deltaTime,game.joueur.getMove(game.PlayerHaveBall),game.ia.getMove(game.IaHaveBall)); 
        
        HandlePlayerMove();
    }
  

    
}