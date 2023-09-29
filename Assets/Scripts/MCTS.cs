using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class MCTS
{
   private int maxIterations;
    private GameState rootState;
    
    public MCTS(int maxIterations)
    {
        this.maxIterations = maxIterations;
    }




    private void Backpropagate(Node node, float result)
    {
        while (node != null)
        {
            node.Update(result);
            node = node.parent;
        }
    }

}