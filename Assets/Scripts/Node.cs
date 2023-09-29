using System.Collections.Generic;
using DefaultNamespace;

public class Node
{
    public IMouvement.Movement move;
    public Node parent;
    public List<Node> children;
    public int wins;
    public int visits;

    public Node(IMouvement.Movement m, Node p)
    {
        move = m;
        parent = p;
        children = new List<Node>();
        wins = 0;
        visits = 0;
    }

    public void ExpandNode(GameState state)
    {
        if (!state.Fin())
        {
            List<IMouvement.Movement> legalMoves = state.ReturnLegalMove();
            foreach (IMouvement.Movement m in legalMoves)
            {
                Node nc = new Node(m, this);
                children.Add(nc);
            }
        }
    }

    public void Update(int r)
    {
        visits++;
        if (r == 1) // Assuming "win" is defined as 1
        {
            wins++;
        }
    }

    public bool IsLeaf()
    {
        return children.Count == 0;
    }

    public bool HasParent()
    {
        return parent != null;
    }
}

