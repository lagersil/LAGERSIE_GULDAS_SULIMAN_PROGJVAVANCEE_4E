using UnityEngine;

[System.Serializable]
public class GameState
{
    public Vector3 positionJoueur;

    public Vector3 positionIA;

    public bool joueurPossedeLaBalle;

    public GameState()
    {
        positionJoueur = Vector3.zero;
        positionIA = Vector3.zero;
        joueurPossedeLaBalle = false;
    }

    public void Reinitialiser()
    {
        positionJoueur = Vector3.zero;
        positionIA = Vector3.zero;
        joueurPossedeLaBalle = false;
    }

    // Méthode pour charger l'état de jeu
    public void Charger()
    {
        positionJoueur.x = PlayerPrefs.GetFloat("PositionJoueurX", 0f);
        positionJoueur.y = PlayerPrefs.GetFloat("PositionJoueurY", 0f);
        positionJoueur.z = PlayerPrefs.GetFloat("PositionJoueurZ", 0f);

        positionIA.x = PlayerPrefs.GetFloat("PositionIAX", 0f);
        positionIA.y = PlayerPrefs.GetFloat("PositionIAY", 0f);
        positionIA.z = PlayerPrefs.GetFloat("PositionIAZ", 0f);

        joueurPossedeLaBalle = PlayerPrefs.GetInt("JoueurPossedeLaBalle", 0) == 1;
    }
}