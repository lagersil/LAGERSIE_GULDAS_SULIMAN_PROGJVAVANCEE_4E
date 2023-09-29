using System;
using System.Collections.Generic;
using System.Xml.Schema;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;
using Random = System.Random;

[System.Serializable]
public struct GameState
{
    public CharacterControll joueur;
    public Joueur2 humain2;
    public IA ia;
    public Ball balle;
    public MCTS mcts; 
    private const float moveSpeed = 5.0f;
    private const float moveBall = 15.0f;
    public bool IaHaveBall; 
    public bool PlayerHaveBall; 
    private const float minZ = 5.0f;
    private const float maxZ = 12.0f;
    private const float minX = 1.0f;
    private const float maxX = 8.0f;
    public bool victoireJoueur;
    public bool victoireIA;
    private bool finDePartie;
    public ChoicePlayer choix;
    public string joueur2;
    private IMouvement.Movement lastAction; 
    Random random ;
    public List<IMouvement.Movement> coupsPossible;


     /**
     * Dans cette fonction, nous regardons le choix que l'utilisateur fait en début de partie
     * Il a le choix entre trois modes: Joueur vs Ia, Joueur vs MCTS ou Joueur vs Joueur
     */
    public string ChoixJoueur2()
    {
        joueur2 = choix.choix();
        return joueur2;
    }
   
    /**
    * La fonction Tick est notre fonction principale, elle est appelée à chaque frame et contient tous les mouvements du jeu. C'est ici qu'est géré le déplacement des joueurs, de la balle, les collisions, les victoires et les défaites.
    * Les mouvements ne sont pas encore encore synchronisés avec les objets de la scène, c'est à dire que les mouvements sont calculés mais pas encore appliqués.
    */
    public void Tick(float delta, IMouvement.Movement mouvementJ, IMouvement.Movement mouvementI)
    {
        switch (mouvementJ)
        {
            case IMouvement.Movement.Up:
                joueur.position.center += Vector3.forward * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Down:
                joueur.position.center += - Vector3.forward * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Left:
                joueur.position.center += - Vector3.right * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Right:
                joueur.position.center +=  Vector3.right * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.ShootDown:
                balle.direction= new Vector3(1,0,-1).normalized;
                break;
            case  IMouvement.Movement.ShootFront:
                balle.direction= new Vector3(1,0,0).normalized;
                break;
            case  IMouvement.Movement.ShootUp:
                balle.direction= new Vector3(1,0,1).normalized;
                break;
            case  IMouvement.Movement.Shoot:
                PlayerHaveBall = false;
                IaHaveBall = false; 
                break;
            case  IMouvement.Movement.None:
                
                break;
        }

        joueur.position.center = new Vector3(Mathf.Clamp(joueur.position.center.x, -6, 1),
            Mathf.Clamp(joueur.position.center.y, 0.99f, 0.99f),
            Mathf.Clamp(joueur.position.center.z, minZ, maxZ));

   

        if (joueur2 == "MCTS")
        {
            MouvementJoueur(ref mcts.position, mouvementI,delta);
           
        }
        else if(joueur2=="IA")
        {
            MouvementJoueur(ref ia.position, mouvementI,delta);
            
        }
        else if(joueur2=="Humain")
        {
            MouvementJoueur(ref humain2.position, mouvementI,delta);
             // Appel de la méthode Fixer
        }
        if (!(PlayerHaveBall || IaHaveBall))
        {
            balle.position.center += balle.direction * (moveBall* delta);
        }

        fixer(ref humain2.position, ref mcts.position,ref ia.position);
        But();
        Rebond();
        Fin();
        lastAction = mouvementI;

    }
    /**
    * Cette fonction utilise des références de Bounds pour gérer le changement du deuxième joueur. On utilise des références pour pouvoir modifier la position du joueur correct dans le Tick 
    */
    public void MouvementJoueur(ref Bounds position, IMouvement.Movement mouvement, float delta)
    {
        switch (mouvement)
        {
            case IMouvement.Movement.Up:
                position.center += Vector3.forward * (moveSpeed * delta);
                break;
            case IMouvement.Movement.Down:
                position.center += -Vector3.forward * (moveSpeed * delta);
                break;
            case IMouvement.Movement.Left:
                position.center += -Vector3.right * (moveSpeed * delta);
                break;
            case IMouvement.Movement.Right:
                position.center += Vector3.right * (moveSpeed * delta);
                break;
            case IMouvement.Movement.ShootDown:
                balle.direction = new Vector3(-1, 0, -1).normalized;
                break;
            case IMouvement.Movement.ShootFront:
                balle.direction = new Vector3(-1, 0, 0).normalized;
                break;
            case IMouvement.Movement.ShootUp:
                balle.direction = new Vector3(-1, 0, 1).normalized;
                break;
            case IMouvement.Movement.Shoot:
                PlayerHaveBall = false;
                IaHaveBall = false;
                break;
            case IMouvement.Movement.None:
                break;
        }
         position.center = new Vector3(Mathf.Clamp(position.center.x,1 , 8),
            Mathf.Clamp(position.center.y, 0.99f, 0.99f),
            Mathf.Clamp(position.center.z, minZ, maxZ));


        
    }
  /**
    * Cette fonction est le constructeur du GameState, elle initialise les variables du GameState.
  */
    public GameState(CharacterControll joueur, IA ia, Ball balle)
    {
        this.joueur=joueur ;
        this.ia=ia;
        this.balle = balle;
        this.PlayerHaveBall = false;
        this.IaHaveBall = false;
        this.victoireJoueur = false;
        this.victoireIA = false;
        this.finDePartie = false;
        this.coupsPossible=new List<IMouvement.Movement>();
        this.random = new Random();
        this.mcts = new MCTS(5);
        this.lastAction = IMouvement.Movement.None;
        this.choix = new ChoicePlayer();
        this.humain2 = new Joueur2();
        this.joueur2 = choix.choix();
    }
    // ReSharper disable Unity.PerformanceAnalysis
    /*
    * Cette fonction a pour but de vérifier si la balle est dans le but ou non. Si elle est dans le but, on met fin à la partie et on affiche le gagnant.
    */
    private void But()
    {
        if (balle.position.center.x>7f) 
        {

    
            victoireJoueur = true;
            victoireIA = false;
            finDePartie = true; 
          

        }
        else if (balle.position.center.x<-6f)
        {
           
            victoireJoueur = false;
            victoireIA = true;
            finDePartie = true; 

        }
    }

    /*
    * Cette fonction a pour but de vérifier si la balle touche un mur pour la faire rebondir du sens inverse de son arrivée.
    */
    private void Rebond()
    {
        
        if (balle.position.center.z <= 5.0||balle.position.center.z >= 12.0)
        {
        
            balle.direction.z*= -1;


        }
    }

    /**
        Cette fonction retourne la fin de partie 
    */
   
    public bool Fin()
    {
        
        Debug.Log(finDePartie);
        return finDePartie;
    }

    public IMouvement.Movement rnd(List<IMouvement.Movement> mouv)
    {

        int nombreAleatoire = UnityEngine.Random.Range(0, 8);
        return mouv[nombreAleatoire]; 
    }
    

    /**
    Cette fonction est appellée pour retourner tous les mouvements possibles pour chaque joueur. IaHaveBall concerne tous les deuxièmes joueurs, que ce soit l'IA, le joueur2 ou le MCTS.
    */
    public List<IMouvement.Movement> ReturnLegalMove()
    {
        List<IMouvement.Movement> coupsPossible = new List<IMouvement.Movement>();
        if (!PlayerHaveBall)
        {
            for (int i = 0; i <= 3; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
            
        }
        if (!IaHaveBall)
        {
            for (int i = 0; i <= 3; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }

        }
        else if(PlayerHaveBall)
        {
            for (int i = 4; i <= 8; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
        }
        else if (IaHaveBall)
        {
            for (int i = 4; i <= 8; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
        }
        coupsPossible.Add(IMouvement.Movement.None);
        return coupsPossible; 
    }

    /**
     * Cette fonction permet de fixer la balle au joueur si elle touche le touche. Ceci simule le fait d'attraper la balle
    */
    public void fixer(ref Bounds joueurPosition, ref Bounds mctsPosition,ref Bounds iaPosition)
    {
        if (joueurPosition.Intersects(balle.position))
        {
            balle.position.center = joueurPosition.ClosestPoint(balle.position.center) - new Vector3(balle.position.extents.x, 0, 0);
           
            PlayerHaveBall = false;
            IaHaveBall = true; 
        }
        else if (mctsPosition.Intersects(balle.position))
        {
            balle.position.center = mctsPosition.ClosestPoint(balle.position.center) - new Vector3(balle.position.extents.x, 0, 0);
     
            PlayerHaveBall = false;
            IaHaveBall = true; 
        }
        else if (iaPosition.Intersects(balle.position))
        {
            balle.position.center = iaPosition.ClosestPoint(balle.position.center) - new Vector3(balle.position.extents.x, 0, 0);
         
            PlayerHaveBall = false;
            IaHaveBall = true; 
        }

        else if(joueur.position.Intersects(balle.position))
        { 
        
            balle.position.center = joueur.position.ClosestPoint(balle.position.center) + new Vector3(balle.position.extents.x, 0, 0);
            PlayerHaveBall = true;
            IaHaveBall = false; 
        }
    }
    /**
        Constructeur par copie de la classe GameState
    */
    public GameState(GameState Copy)
    {
        joueur=Copy.joueur ;
        ia=Copy.ia;
        balle = Copy.balle;
        PlayerHaveBall = false;
        IaHaveBall = false;
        victoireJoueur = false;
        victoireIA = false;
        finDePartie = false;
        coupsPossible=new List<IMouvement.Movement>();
        random = new Random();
        mcts = new MCTS(5);
        lastAction = IMouvement.Movement.None;
        this.choix = new ChoicePlayer();
        this.humain2 = new Joueur2();
        this.joueur2 = choix.choix();
       
    }
    /**
        Retourne le dernier mouvement
    */
    public IMouvement.Movement GetLastAction()
    {
        return lastAction;
    }
}