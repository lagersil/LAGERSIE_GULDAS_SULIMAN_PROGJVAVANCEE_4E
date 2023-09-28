using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private GameState state; // L'état du jeu représenté par ce nœud
    private Action action; // L'action qui a conduit à cet état depuis le parent
    private Node parent; // Le nœud parent
    private List<Node> children = new List<Node>(); // Les nœuds enfants

    private int visits = 0; // Le nombre de visites de ce nœud
    private float value = 0; // La valeur accumulée pour ce nœud

    public Node(GameState state, Action action = null, Node parent = null)
    {
        this.state = state;
        this.action = action;
        this.parent = parent;
    }

    // Méthodes pour accéder aux membres privés
    public GameState GetState()
    {
        return state;
    }

    public Action GetAction()
    {
        return action;
    }

    public Node GetParent()
    {
        return parent;
    }

    public List<Node> GetChildren()
    {
        return children;
    }

    public int GetVisits()
    {
        return visits;
    }

    public float GetValue()
    {
        return value;
    }

    // Méthode pour ajouter un nœud enfant
    public void AddChild(Node child)
    {
        children.Add(child);
    }

    // Méthode pour mettre à jour les statistiques de visite et de valeur
    public void UpdateStats(float result)
    {
        visits++;
        value += result;
    }

    // Méthode pour vérifier si le nœud a des actions non explorées
    public bool HasUntriedActions()
    {
        
    }

    // Méthode pour obtenir une action non explorée
    public Action GetUntriedAction()
    {
        
    }

    // Méthode pour vérifier si le nœud a des enfants
    public bool HasChildren()
    {
        return children.Count > 0;
    }
}
