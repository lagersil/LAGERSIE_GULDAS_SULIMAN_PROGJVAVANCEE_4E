using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MCTS
{
    private Node rootNode;
    private int maxIterations;
    public Bounds position; 
    public MCTS(int maxIterations)
    {
        this.maxIterations = maxIterations;
    }

    public IMouvement.Movement FindBestMove(GameState initialState)
    {
        rootNode = new Node(IMouvement.Movement.None, null);

        for (int i = 0; i < maxIterations; i++)
        {
            Node selectedNode = Selection(rootNode);
            Node expandedNode = Expansion(selectedNode, initialState);
            int simulationResult = Simulation(expandedNode, initialState);
            Backpropagation(expandedNode, simulationResult);
        }

       
        return SelectBestMove(rootNode);
    }

    private Node Selection(Node node)
    {
        if (node.IsLeaf())
        {
            return node; // Si le nœud est une feuille, le sélectionner directement
        }
        else
        {
            List<Node> unexploredChildren = new List<Node>();
            List<Node> exploredChildren = new List<Node>();

            foreach (Node child in node.children)
            {
                if (child.visits == 0)
                {
                    unexploredChildren.Add(child); 
                }
                else
                {
                    exploredChildren.Add(child); 
                }
            }

            if (unexploredChildren.Count > 0)
            {
                return unexploredChildren[UnityEngine.Random.Range(0, unexploredChildren.Count)]; // Sélectionnez aléatoirement parmi les nœuds non explorés
            }
            else
            {
                return exploredChildren[UnityEngine.Random.Range(0, exploredChildren.Count)]; // Sélectionnez aléatoirement parmi les nœuds explorés
            }
        }
    }

    private Node Expansion(Node node, GameState initialState)
    {
        node.ExpandNode(initialState);
        if (node.children.Count > 0)
        {
            return node.children[UnityEngine.Random.Range(0, node.children.Count)];
        }
        return node;
    }

    private int Simulation(Node node, GameState initialState)
    {
        GameState currentState = new GameState(initialState); 

            while (!currentState.Fin()) 
            {
                List<IMouvement.Movement> legalMoves = currentState.ReturnLegalMove();
              
                if (legalMoves.Count > 0)
                {
                    int randomMoveIndex = UnityEngine.Random.Range(0, legalMoves.Count);
                    IMouvement.Movement randomMove = legalMoves[randomMoveIndex];
                    currentState.Tick(1f / 60f, IMouvement.Movement.None, randomMove);
                    
                }
            }
          
            if (currentState.victoireIA)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        
    }

    private void Backpropagation(Node node, int result)
    {
        while (node != null)
        {
            node.Update(result);
            node = node.parent;
        }
    }

    private IMouvement.Movement SelectBestMove(Node rootNode)
    {
        if (rootNode.children.Count > 0)
        {
            int randomChildIndex = UnityEngine.Random.Range(0, rootNode.children.Count);
            Node randomChild = rootNode.children[randomChildIndex];
            return randomChild.move;
        }

      
        return IMouvement.Movement.None;
    }
}