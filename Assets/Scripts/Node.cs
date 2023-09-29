using System.Collections.Generic;
using DefaultNamespace;

//Class
public class Node
{
    public GameState state;
    public IMouvement.Movement move;
    public Node parent;
    public List<Node> children;
    public int wins;
    public int visits, score;

    // Constructeur de la classe Node.
    public Node(List<Node> child, Node p, GameState s, int scor)
    {
        state = s;
        parent = p;
        children = new List<Node>();
        wins = 0;
        visits = 0;
        score = scor;
    }

    // Méthode pour obtenir le score total en tenant compte des scores des enfants.
    public int GetScore()
    {
        int sco = score;

        foreach (var n in children)
        {
            sco += n.score;
        }

        return sco;
    }

    // Méthode pour étendre ce nœud en ajoutant des nœuds enfants basés sur les mouvements légaux.
    public void ExpandNode(GameState state)
    {
        if (!state.Fin())
        {
            List<IMouvement.Movement> legalMoves = state.ReturnLegalMove();
            foreach (IMouvement.Movement m in legalMoves)
            {
                Node nc = new Node(null, this, state, 0); 
                children.Add(nc);
            }
        }
    }
    
    // Méthode pour mettre à jour les statistiques du nœud après une simulation.
    public void Update(int r)
    {
        visits++;
        if (r == 1) 
        {
            wins++;
        }
    }

   
}