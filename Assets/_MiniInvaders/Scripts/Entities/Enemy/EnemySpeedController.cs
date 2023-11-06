using UnityEngine;

public class EnemySpeedController:MonoBehaviour
{
    public float Speed { get { return direction * speed; } }

    public bool IsMoving { get; private set; }

    [SerializeField]
    private float initialSpeed = .5f;

    [SerializeField]
    private AnimationCurve difficulty;

    private int direction = 1;
    private float speed;
    private float baseSpeed;

    public void DefineInitialSpeed(int level)
    {
        baseSpeed = level * initialSpeed;
        speed = difficulty.Evaluate(0) + baseSpeed;
    }
    
    public void IncreaseSpeed(float percentOfEnemyKills)
    {
        speed = difficulty.Evaluate(percentOfEnemyKills) + baseSpeed;
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
