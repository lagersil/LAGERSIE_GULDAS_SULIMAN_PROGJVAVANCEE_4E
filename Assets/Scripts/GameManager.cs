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
    public GameObject arrowGO;
    //private ArrowController arrowController;
    
    private GameState game;

    private void Start()
    { 
        game = new GameState();
        game.joueur = new CharacterControll();
        game.ia = new IA();
        game.balle = new Ball();
       
        game.joueur.position.center = joueurGO.transform.position;
        game.ia.position.center = iaGO.transform.position;
        game.balle.position.center = balleGO.transform.position;
        game.joueur.position.size = joueurGO.transform.lossyScale;
        game.ia.position.size = iaGO.transform.lossyScale;
        game.balle.position.size = balleGO.transform.lossyScale;
        //tout initialiser correctement (size etc)

    }
    void HandlePlayerMove()
    {
  
        joueurGO.transform.position = game.joueur.position.center;
        iaGO.transform.position = game.ia.position.center;
        balleGO.transform.position = game.balle.position.center;
    }
 
    void Update()
    {
        game.Tick(Time.deltaTime); 
        HandlePlayerMove();
    }
    
    /*Condition victoire à vérifier
     *
     *
     *
     * 
     */

    
}