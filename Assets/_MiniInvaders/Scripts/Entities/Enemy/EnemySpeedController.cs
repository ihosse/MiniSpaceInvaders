using UnityEngine;

public class EnemySpeedController:MonoBehaviour
{
    public float Speed { get { return direction * speed; } }

    public bool IsMoving { get; private set; }

    [SerializeField]
    private AnimationCurve speedBase;

    private int direction = 1;
    private float speed;

    public void DefineInitialSpeed()
    {
        speed = speedBase.Evaluate(0);
    }
    
    public void IncreaseSpeed(float percentOfEnemyKills)
    {
        speed = speedBase.Evaluate(percentOfEnemyKills);
        print(percentOfEnemyKills + " " + speed);
    }

    public void ActivateMovement(bool value)
    {
        IsMoving = value;
    }

    public void InvertSpeed()
    {
        direction *= -1;
    }
}
