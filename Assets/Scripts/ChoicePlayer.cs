using UnityEngine;

namespace DefaultNamespace
{
    // Classe pour gérer le choix du type de joueur
    public class ChoicePlayer
    {
        private int joueur;
        private int IA;
        private int MCTS;

       
        // Constructeur de la classe
        public ChoicePlayer()
        {
            IA = PlayerPrefs.GetInt("IA");
            MCTS = PlayerPrefs.GetInt("MCTS");
            joueur = PlayerPrefs.GetInt("Humain");
            choix();
        }

        // Methode pour determiner le type de joueur en fonction des PlayerPrefs
        public string choix()
        {
            if (IA == 1)
            {
                return "IA";
            }
            else if (MCTS == 1)
            {
                return "MCTS";
            }
            else if (joueur == 1)
            {
                return "Humain";
            }

            return null; 
        }
    }
}