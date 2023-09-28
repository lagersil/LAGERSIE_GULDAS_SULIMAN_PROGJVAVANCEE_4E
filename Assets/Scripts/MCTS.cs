using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCTS
	private GameState game;
	private CharacterControll Player;
	private IA ia;
	private Ball balle;
	private Mouvement move;

{
    public Node select(Node node)
	{
		while (!node.IsTerminal() && node.HasUntriedActions())
		{
			return Expand(node);
		}

		if(node.HasChildren())
		{
			Node bestChild = SelectBestChild(Node);
			return Select(bestChild);
		}

		return node;
	}


	//UCB (Upper Confidence Bound) permet de vérifier qui est le meilleur enfant lors de la sélection
	private Node SelectBestChild(Node node)
	{
		float explorationWeight = Mathf.Sqrt(2.0f);

		Node bestChild = null;
		float bestUBD = float.MinValue;

		foreach(Node child in node.GetChildren())
		{
			float UCB = (child.GetValue() / child.GetVisite() + explorationWeight * Mathf.Sqrt(Mathf.Log(node.GetVisits()) / child.GetVisits());
			
			if (UCB > bestUCB)
            {
                bestUCB = UCB;
                bestChild = child;
            }
        }

        return bestChild;
		}
	}


	 public Node Expand(Node node)
    {
        if (node.HasUntriedActions())
        {
            Action untriedAction = node.GetUntriedAction();

            GameState newState = node.GetState().ApplyAction(untriedAction);

            Node newChild = new Node(newState, untriedAction, node);

            node.AddChild(newChild);

            return newChild;
        }
        else
        {
            throw new InvalidOperationException("Toutes les actions ont déjà été explorées.");
        }
    }

	public void RunMCTS(Game initialState, int iterations)
	{
		Node root = new Node(initialState);
		
		for(int i = 0; i < iterations; i++)
		{
			Node selectedNode = Select(root);
            Node expandedNode = Expand(selectedNode);
            float simulationResult = Simulate(expandedNode);
            Backpropagate(expandedNode, simulationResult);
		}

		Node bestChild = SelectBestChild(root);
        return bestChild.Action;

	}
}
