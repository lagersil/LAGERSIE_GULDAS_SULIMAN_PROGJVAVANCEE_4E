using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class MCTS
{
    private int maxIterations;
    public Bounds position; 
    public MCTS(int maxIterations)
    {
        this.maxIterations = maxIterations;
    }

    /**
        Ceci est une implémentation de l'algorithme MCTS( Monte Carlo Tree Search) qui permet de trouver le meilleur mouvement à effectuer dans un jeu.
        On sélectionne un noeud,
        On étend l'arbre en ajoutant un noeud enfant,
        On simule le jeu jusqu'à la fin,
        On met à jour les statistiques des noeuds visités.
    */

    public IMouvement.Movement MonteCarlo(GameState initialState)
    {
        Node root= new Node(null,null,initialState,0);
        List<Node> nodes = new List<Node>();
        nodes.Add((root));
        for (int i = 0; i < maxIterations; i++)
        {
           
            Node selectedNode = Selection(root);
            Node expandedNode = Expansion(selectedNode, initialState);
            int simulationResult = Simulation(expandedNode, initialState, 5);
            nodes.Add(expandedNode);
            Backpropagation(expandedNode, simulationResult);
        }

       
        return SelectBestMove(nodes.ToArray());
    }

    /**
        Cette fonction permet de sélectionner le prochain noeud à explorer.
        On sélectionne par la suite un noeud à explorer ou à exploiter.
        Lorsqu'on exploite un noeud, on choisi un noeud déjà existant qui semble avoir la meilleure probabilité de victoire.
        Lorsqu'on explore un noeud, on choisi un noeud qui n'a pas encore été exploré de façon aléatoire.


    */
    private Node Selection(Node node)
    {
        if (node.children.Count==0)
        {
            return node; 
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
                return unexploredChildren[UnityEngine.Random.Range(0, unexploredChildren.Count)]; 
            }
            else
            {
                return exploredChildren[UnityEngine.Random.Range(0, exploredChildren.Count)]; 
            }
        }
    }

    /**
        L'expansion intervient après la phase de sélection. 
        Le but est d"ajouter un noeud enfant au noeud sélectionné.
        L'arbre va donc s'étendre pour inclure de nouvelles branches qui correspondent à de nouveaux coups possibles.

    */
    private Node Expansion(Node node, GameState initialState)
    {
        node.ExpandNode(initialState);
        if (node.children.Count > 0)
        {
            return node.children[UnityEngine.Random.Range(0, node.children.Count)];
        }
        return node;
    }


    /**
        La simulation permet d'estimer la qualité d'une action à partir d'un noeud. 
        Le MCTS va venir simuler un jeu complet à partir du noeud sélectionné jusqu'à atteindre une condition de fin ou une profondeur maximale.
        A la fin de la simulation, on retourne le score de la simulation.
        Cette simulation est répétée pluseurs fois à partir du même noeud. 
    */
    private int Simulation(Node node, GameState initialState, int depth)
    {
        if (depth == 0) return 0;
        List<IMouvement.Movement> legalMoves = initialState.ReturnLegalMove();
        List<Node> nodes = new List<Node>();

        for (int j = 0; j < legalMoves.Count / 4; j++)
        {
            GameState currentState = initialState;
            int randomMoveIndex = j;
            currentState.Tick(1f / 60f, IMouvement.Movement.None, legalMoves[randomMoveIndex]);

            int score = 0;
            if (currentState.PlayerHaveBall)
                score += -1;

            if (currentState.IaHaveBall)
                score += 1;

            if (currentState.victoireJoueur)
                score += -2;

            if (currentState.victoireIA)
                score += 2;

            Node n = new Node(null, node, currentState, score);
            nodes.Add(n);
        }

        node.children = nodes;

        Node[] arrNodes = nodes.ToArray();

        Array.Sort(arrNodes, (node1, node2) => Random.Range(-1, 1));

        for (int i = 0; i < arrNodes.Length / 2; i++)
            Simulation(node, arrNodes[i].state, depth - 1);

        return 0; 
    }


   /**
        Cette fonction va permettre la mise à jour des statistiques des noeuds parents.
        On va donc remonter l'arbre en mettant à jour les statistiques.
        Le nombre de visites et le score vont être incrémentés et mis à jour. 
        On remonte jusqu'à la racine de l'arbre.
   */
    private void Backpropagation(Node node, int result)
    {
        while (node != null)
        {
            node.Update(result);
            node = node.parent;
        }
    }

    /**
        Cette fonction permet de sélectionner le meilleur mouvement donc le meilleur score.
        On retourne le dernier mouvement effectué par le noeud.
    */
    private IMouvement.Movement SelectBestMove(Node[] nodes)
    {
        Node nod = null;
        
        foreach (Node n in nodes)
        {
            if (nod == null || nod.GetScore() < n.GetScore())
            {
                nod = n;
            }
        }

      
        return nod != null? nod.state.GetLastAction() : IMouvement.Movement.None;
    }
}