using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

[System.Serializable]
public struct GameState
{
    public CharacterControll joueur;
    public IA ia;
    public Ball balle;
    public MCTS mcts; 
    private const float moveSpeed = 5.0f;
    public bool IaHaveBall; 
    public bool PlayerHaveBall; 
    private const float minZ = 5.0f;
    private const float maxZ = 12.0f;
    private const float minX = 1.0f;
    private const float maxX = 8.0f;
    public bool victoireJoueur;
    public bool victoireIA;
    private bool finDePartie;
    private IMouvement.Movement lastAction; 
    Random random ;
    public List<IMouvement.Movement> coupsPossible; 
   
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
        
        
        switch (mouvementI)
        {
            case IMouvement.Movement.Up:
                ia.position.center += Vector3.forward * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Down:
                ia.position.center += - Vector3.forward * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Left:
                ia.position.center += - Vector3.right * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.Right:
                ia.position.center +=  Vector3.right * (moveSpeed * delta);
                break;
            case  IMouvement.Movement.ShootDown:
                balle.direction= new Vector3(-1,0,-1).normalized;
                break;
            case  IMouvement.Movement.ShootFront:
                balle.direction= new Vector3(-1,0,0).normalized;
                break;
            case  IMouvement.Movement.ShootUp:
                balle.direction= new Vector3(-1,0,1).normalized;
                break;
            case  IMouvement.Movement.Shoot:
                IaHaveBall = false; 
                PlayerHaveBall = false;
                break;
            case  IMouvement.Movement.None:
             
                break;
        }

        mcts.position.center = new Vector3(Mathf.Clamp(mcts.position.center.x, minX, maxX),
            Mathf.Clamp(mcts.position.center.y, 0.99f, 0.99f),
            Mathf.Clamp(mcts.position.center.z, minZ, maxZ));

        if (!(PlayerHaveBall || IaHaveBall))
        {
            balle.position.center += balle.direction * (moveSpeed* delta*3);
        }

        fixer();
        But();
        Rebond();
        Fin();
        lastAction = mouvementI;

    }
    
  
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
        this.mcts = new MCTS(30);
        this.lastAction = IMouvement.Movement.None; 
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void But()
    {
        if (balle.position.center.x>7f) 
        {

            Debug.Log("Points pour joueur");
            victoireJoueur = true;
            victoireIA = false;
            //Panel_Win.SetActive(true);
            //SceneManager.LoadScene("MainMenu");
        }
        else if (balle.position.center.x<-6f)
        {
            Debug.Log("Points pour Ia");
            victoireJoueur = false;
            victoireIA = true;
            
        }
    }
    private void Rebond()
    {
        
        if (balle.position.center.z <= 5.0||balle.position.center.z >= 12.0)
        {
        
            balle.direction.z*= -1;


        }
    }

   
    public bool Fin()
    {
        return finDePartie = true; 
    }

    public IMouvement.Movement rnd(List<IMouvement.Movement> mouv)
    {

        int nombreAleatoire = UnityEngine.Random.Range(0, 8);
        return mouv[nombreAleatoire]; 
    }
    public List<IMouvement.Movement> ReturnMove()
    {
        List<IMouvement.Movement> coupsPossible = new();
        if (!PlayerHaveBall)
        {
            for (int i = 0; i <= 4; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
            
        }
        if (!IaHaveBall)
        {
            for (int i = 0; i <= 4; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }

        }
        else if(PlayerHaveBall)
        {
            for (int i = 5; i <= 8; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
        }
        else if (IaHaveBall)
        {
            for (int i = 5; i <= 8; i++)
            {
                IMouvement.Movement mouv = (IMouvement.Movement)i;
                coupsPossible.Add(mouv);
            }
        }
        coupsPossible.Add(IMouvement.Movement.None);
        return coupsPossible; 
    }
    public void fixer()
    {
        if (joueur.position.Intersects(balle.position))
        {
            balle.position.center = joueur.position.ClosestPoint(balle.position.center)+ new Vector3(balle.position.extents.x, 0, 0);
            PlayerHaveBall = true;
            IaHaveBall = false; 
        }
        else if (ia.position.Intersects(balle.position))
        {
            balle.position.center = ia.position.ClosestPoint(balle.position.center)- new Vector3(balle.position.extents.x, 0, 0);
            PlayerHaveBall = false;
            IaHaveBall = true; 
        }
        else
        {
            PlayerHaveBall = false;
            IaHaveBall = false; 
        }
    }
    
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
        mcts = new MCTS(30);
        lastAction = IMouvement.Movement.None;
       
    }
    public IMouvement.Movement GetLastAction()
    {
        return lastAction;
    }
}