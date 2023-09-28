namespace DefaultNamespace
{
    public interface IMouvement
    {
        
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

    Movement getMove(bool balle);

    }
}