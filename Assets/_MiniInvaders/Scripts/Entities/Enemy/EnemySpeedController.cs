public class EnemySpeedController
{
    public float Speed { get; private set; }

    public bool IsMoving { get; private set; }

    public void DefineInitialSpeed(float speed)
    {
        Speed = speed;
    }
    
    public void IncreaseSpeed(float multiplier)
    {
        Speed *= multiplier;
    }

    public void ActivateMovement(bool value)
    {
        IsMoving = value;
    }

    public void InvertSpeed()
    {
        Speed *= -1;
    }
}
