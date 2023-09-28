using DefaultNamespace;
using UnityEngine;

[System.Serializable]
public struct GameState
{
    
    /* delta time gamemanager
     * vitesse de simu gamestate
     */
    
    public CharacterControll joueur;
    public IA ia;
    public Ball balle;
    private const float moveSpeed = 5.0f;
    private bool tirer;
   // public Bounds arrow;
   //Faire constructeur 
    private bool IaHaveBall; 
    private bool PlayerHaveBall; 
    private const float minZ = 5.0f;
    private const float maxZ = 12.0f;
    private const float minX = 1.0f;
    private const float maxX = 8.0f;


    /*Condition victoire défaite à mettre en place
     *
     *
     *
     *
     * 
     */
    public void Tick(float delta)
    {
        switch (joueur.getMove(PlayerHaveBall))
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
        
        
        switch (ia.getMove(IaHaveBall))
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

        ia.position.center = new Vector3(Mathf.Clamp(ia.position.center.x, minX, maxX),
            Mathf.Clamp(ia.position.center.y, 0.99f, 0.99f),
            Mathf.Clamp(ia.position.center.z, minZ, maxZ));

        if (!(PlayerHaveBall || IaHaveBall))
        {
            balle.position.center += balle.direction * (moveSpeed* delta*3);
        }

        fixer();
        But();
        Rebond();

    }
    public GameState(CharacterControll joueur, IA ia, Ball balle)
    {
        this.joueur=joueur ;
        this.ia=ia;
        this.balle = balle;
        this.PlayerHaveBall = false;
        this.IaHaveBall = false;
        this.tirer = false;
    }
    // ReSharper disable Unity.PerformanceAnalysis
    private void But()
    {
        if (balle.position.center.x>7f) 
        {

            Debug.Log("Points pour joueur");
            //Panel_Win.SetActive(true);
            //SceneManager.LoadScene("MainMenu");
        }
        else if (balle.position.center.x<-6f)
        {
            Debug.Log("Points pour Ia");
            //Panel_Lose.SetActive(true);
            //SceneManager.LoadScene("MainMenu");
        }
    }
    private void Rebond()
    {
        
        if (balle.position.center.z <= 5.0||balle.position.center.z >= 12.0)
        {
        
            balle.direction.z*= -1;
            //balle.direction.x-= balle.direction.x;

        }
      
        
         
    }

    public void victoire()
    {
        //A FAIRE;
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
  /*  public void DetachBall(float angle, float delta)
    {
        if(tirer==true){
            float launchAngleRad = angle * Mathf.Deg2Rad;
            Vector3 launchDirection = new Vector3(Mathf.Cos(launchAngleRad), 0.0f, Mathf.Sin(launchAngleRad));
         
            float launchForce = 10.0f; 
            balle.position.center += launchDirection * launchForce * delta;
        }
    }*/
    
}