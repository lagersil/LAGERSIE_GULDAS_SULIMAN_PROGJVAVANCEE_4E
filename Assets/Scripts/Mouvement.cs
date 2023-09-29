namespace DefaultNamespace
{
    // Définir l'interface IMouvement   
    public interface IMouvement
    {

    // Déclarer l'énumérateur Movement à l'intérieur de l'interface    
    public enum Movement
        {
            Up,
            Down,
            Left,
            Right,
            ShootUp, 
            ShootDown, 
            ShootFront, 
            Shoot,
            None
        }
    
    // Déclarer la méthode getMove qui prend un booléen en paramètre
    Movement getMove(bool balle);

    }
}