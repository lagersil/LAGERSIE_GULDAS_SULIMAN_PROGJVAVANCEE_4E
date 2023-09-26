using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
        public GameObject player; 
        public GameObject iA; 
        public GameObject ball; 
        public int playerScore; 
        public int iAScore;
        public bool finDePartie; 
        public string winner;
        public string hasball; 

        public State(GameObject playerPos, GameObject iAPos, GameObject ballPos, int playerSc, int iaSc, bool fin,string win, string hasballe)
        {
            player = playerPos;
            iA = iAPos;
            ball = ballPos;
            playerScore = playerSc;
            iAScore = iaSc;
            finDePartie = fin;
            winner = win;
            hasball = hasballe; 
        }
    
}
