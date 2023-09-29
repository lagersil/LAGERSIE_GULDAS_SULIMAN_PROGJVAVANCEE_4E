using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AOT;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class MCTS
{
   private int maxIterations;
    private GameState rootState;
    private float pourcentage; 
    IMouvement.Movement selected1 = IMouvement.Movement.None;
    IMouvement.Movement selected2 = IMouvement.Movement.None;
    private int nbWin;
    private Node startNode;
    private float explorationParameter=0.8f;
    public Bounds position;
    public IMouvement.Movement bestBB;
    private List<Node> all; 
    public MCTS(int maxIterations)
    {
        this.maxIterations = maxIterations;
        this.rootState = new GameState();
        this.startNode = new Node(rootState);
        this.all= startNode.GetAllNodes(startNode);
    }

    public IMouvement.Movement Montecarlo()
    {
        startNode = new Node(rootState);

        for (int i = 0; i < maxIterations; ++i)
        {
            Node selectedNode = Select(explorationParameter);
            Node newNode = Expand(selectedNode);
            int victoire = Simulation(30);
            Backpropagate(newNode,victoire);
            
        }
        bestBB = rootState.GetLastAction();
      Debug.Log(bestBB);
      return bestBB;
    }

    

    int Simulation(int nbSimu)
    {
        GameState clone = new GameState(rootState);
        for (int i = 0; i < nbSimu; i++)
        {
            while (!clone.Fin())
            {
                List<IMouvement.Movement> mouvementsIa =  clone.coupsPossible;
                List<IMouvement.Movement> mouvementsJ = clone.coupsPossible;
                selected1 =clone.rnd(mouvementsIa); 
                selected2 =clone.rnd(mouvementsJ);
                clone.Tick(1/60,selected2,selected1);
            }

            if (clone.victoireIA)
            {
                nbWin++;
            }
        }

        return nbWin; 
    }

    Node Select(float explorationParameter)
    {
        int rnd = UnityEngine.Random.Range(0, all.Count());
        
        pourcentage = UnityEngine.Random.Range(0f, 1f);
        if (pourcentage > explorationParameter)
        {
            startNode = all[rnd];
        }
        else
        {
            Node bestChild = startNode.GetBestChild();
            startNode = bestChild;
        }
        

        return startNode; 
    }

    Node Expand(Node node)
    {
     
        
        foreach (IMouvement.Movement move in rootState.ReturnMove())
        {
            GameState newState = new GameState(rootState);
            newState.Tick(1/60,IMouvement.Movement.None,move);
            Node newChild = new Node(newState);
            node.children.Add(newChild);
        }

        return node; 
    }
    private void Backpropagate(Node node, float result)
    {
        while (node != null)
        {
            node.Update(result,30);
            node = node.parent;
        }
    }
   
}