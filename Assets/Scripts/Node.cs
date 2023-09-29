using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Node
{
    public Node parent;
    public List<Node> children;
    public int visitCount;
    public float totalReward;
    public float ucbValue;
    public GameState state;

    public Node(GameState initialState)
    {
        parent = null;
        children = new List<Node>();
        visitCount = 0;
        totalReward = 0;
        ucbValue = float.MaxValue;
        state = initialState;
    }

    public void Update(float reward)
    {
        visitCount++;
        totalReward += reward;
    }

    public float UCBValue(int parentVisitCount, float explorationParameter)
    {
        if (visitCount == 0)
            return float.MaxValue;

        float exploitation = totalReward / visitCount;
        float exploration = explorationParameter * Mathf.Sqrt(Mathf.Log(parentVisitCount) / visitCount);
        ucbValue = exploitation + exploration;
        return ucbValue;
    }

    public bool IsFullyExpanded()
    {
        return children.Count == state.GetLegalActions().Count;
    }

    public Node GetBestChild(float explorationParameter)
    {
        Node bestChild = null;
        float bestUCBValue = float.MinValue;

        foreach (Node child in children)
        {
            float childUCBValue = child.UCBValue(visitCount, explorationParameter);
            if (childUCBValue > bestUCBValue)
            {
                bestUCBValue = childUCBValue;
                bestChild = child;
            }
        }

        return bestChild;
    }
}