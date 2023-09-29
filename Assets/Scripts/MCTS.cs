using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class MCTS : MonoBehaviour
{
	private GameState game;
	private Node node;
	

	public IMouvement.Movement RunMCTS(GameState initialState, int iterations)
	{
		Node rootNode = new Node(initialState);	
		
		for(int i = 0; i < iterations; i++)
		{
			Node selectedNode = Select(rootNode);
			Node expandedNode = Expand(selectedNode);
			float simulationResult = Simulate(expandedNode);
			Backpropagate(expandedNode, simulationResult);
		}

		Node bestChild = SelectBestChild(root);
		return bestChild.action;

	}
	
    public Node Select(Node nodeRoot)
    {
	    Node node = nodeRoot;

		while (node.GetChildren().Count > 0)
		{
			Node bestChild = SelectBestChild(node);
			return Select(bestChild);
		}

		return node;
	}


	 public Node Expand(Node node)
    {
	    
	    List<IMouvement.Movement> coupsPossible = node.GetState().;

	    foreach (GameState nextState in coupsPossible)
	    {
		    Node newChild = new Node(nextState);
		    newChild.parent = node;
		    node.children.Add(newChild);
	    }
	    Node selectedChild = Select(node);

	    return selectedChild;
       
    }
	 
	 //UCB (Upper Confidence Bound) permet de vérifier qui est le meilleur enfant lors de la sélection
	 private Node SelectBestChild(Node node)
	 {
		 float explorationWeight = Mathf.Sqrt(2.0f);

		 Node bestChild = null;
		 float bestUCB = float.MinValue;

		 foreach (Node child in node.GetChildren())
		 {
			 float UCB = (child.GetValue() / child.GetVisits() +
			              explorationWeight * Mathf.Sqrt(2 * Mathf.Log(node.GetVisits()) / child.GetVisits()));

			 if (UCB > bestUCB)
			 {
				 bestUCB = UCB;
				 bestChild = child;
			 }
		 }

		 return bestChild;
	 }
}
