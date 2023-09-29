using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;


public class Node
{
    public Node parent;
    public List<Node> children;
    public int visitCount;
    public float totalReward;
    public float Value;
    public GameState state;
    public IMouvement.Movement lastMove; 

    
    public List<Node> GetAllNodes(Node rootNode)
    {
        List<Node> allNodes = new List<Node>();
        TraverseTree(rootNode, allNodes);
        return allNodes;
    }
    private void TraverseTree(Node currentNode, List<Node> allNodes)
    {
        allNodes.Add(currentNode);
        foreach (Node child in currentNode.children)
        { 
            TraverseTree(child, allNodes);
        }
    }
    public Node(GameState initialState)
    {
        parent = null;
        children = new List<Node>();
        visitCount = 0;
        totalReward = 0;

        state = initialState;
        lastMove = IMouvement.Movement.None; 
    }

    public void Update(float reward,int nbVisite)
    {
        visitCount+=nbVisite;
        totalReward += reward;
    }

    

    public bool IsFullyExpanded()
    {
        return children.Count == 5;
    }

    public Node GetBestChild()
    {
        Node bestChild = null;
        float bestTotalReward = float.MinValue;

        foreach (Node child in children)
        {
            if (child.totalReward > bestTotalReward)
            {
                bestTotalReward = child.totalReward;
                bestChild = child;
            }
        }

        if (bestChild != null)
        {
            lastMove = bestChild.state.GetLastAction();
        }

        return bestChild;
    }
}
