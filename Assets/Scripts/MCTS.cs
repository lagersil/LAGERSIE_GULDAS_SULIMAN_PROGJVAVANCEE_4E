using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCTS : MonoBehaviour
{
    private Node root;
    
    public GameObject IAPlayer;
    private string player; 
    public GameObject balle;
    private Rigidbody rbIA;
    private float moveSpeed = 5.0f;
    private float smoothTime = 0.5f;
    private float delay = 1.0f; 
    private float minZ = 5.0f;
    private float maxZ = 12.0f;
    private float minX = 1.0f;
    private float maxX = 8.0f;
    private string finDePartie; 
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;


    private void Start()
    {
        finDePartie = PlayerPrefs.GetString("finDePartie");
    }
    public MCTS(State initialState)
    {
        root = new Node(initialState);
    }

    public void MonteCarlo(int iterations)
    {
        for (int i = 0; i < iterations; i++)
        {
            Node selectedNode = Select(root);
            Node expandedNode = Expand(selectedNode);
            //int simulationResult = Simulate(expandedNode);
            //Backpropagate(expandedNode, simulationResult);
        }
    }

    private Node Select(Node node)
    {
        Node selectedChild = null;
        int maxVisits = -1;

        foreach (Node child in node.children)
        {
            if (child.visits > maxVisits)
            {
                maxVisits = child.visits;
                selectedChild = child;
            }
        }

        return selectedChild;
    }

    private Node Expand(Node node)
    {
       
        List<State> coupsPossible = Coups(node.state);

        foreach (State nextState in coupsPossible)
        {
            Node newChild = new Node(nextState);
            newChild.parent = node;
            node.children.Add(newChild);
        }
        Node selectedChild = Select(node);

        return selectedChild;
    }


    private void Backpropagate(Node node, int result)
    {
        Node currentNode = node;
        while (currentNode != null)
        {
            currentNode.visits++;
            currentNode.total += result;
            currentNode = currentNode.parent;
        }
    }

    //noeud de l'arbre MCTS
    private class Node
    {
        public State state;
        public Node parent;
        public List<Node> children;
        public int visits;
        public int total;

        public Node(State state)
        {
            this.state = state;
            this.parent = null;
            this.children = new List<Node>();
            this.visits = 0;
            this.total = 0;
        }
    }
    private enum Movement
    {
        Up,
        Down,
        Left,
        Right
    };

    private enum Shoot
    {
        Up,
        Down,
        Front
    };

    private List<State> Coups(State currentState)
    {
        List<State> coupsPossible = new List<State>();

        foreach (Movement movementAction in Enum.GetValues(typeof(Movement)))
        {
            State newState = GenerateStateAfterMovement(currentState, movementAction);
            coupsPossible.Add(newState);
        }
    
    /*else
    {
        foreach (Shoot movementAction in Enum.GetValues(typeof(Shoot)))
        {
            //State newStateAfterThrow = GenerateStateAfterThrow(currentState);
            //coupsPossible.Add(newStateAfterThrow);
        }
        
    }*/


    return coupsPossible;
}
    private State GenerateStateAfterMovement(State st, Movement mouv)
    {
        
        switch (mouv)
        {
            case mouv.Up:
                targetPosition = transform.position + Vector3.forward * moveSpeed;
                break;
            case mouv.Down:
                targetPosition = transform.position - Vector3.forward * moveSpeed;
                break;
            case mouv.Left:
                targetPosition = transform.position - Vector3.right * moveSpeed;
                break;
            case mouv.Right:
                targetPosition = transform.position + Vector3.right * moveSpeed;
                break;
        }
 
       
        targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, 0.99f, 0.99f);
        
        State newState = new State(st.player, st.iA, st.ball, st.playerScore, st.iAScore, st.finDePartie, st.winner,st.hasball);
        return newState;
    }
    }

