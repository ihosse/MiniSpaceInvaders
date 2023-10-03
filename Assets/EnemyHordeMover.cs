using System;
using UnityEngine;

public class EnemyHordeMover : MonoBehaviour 
{
    [SerializeField]
    private float initialHorizontalSpeed = 1;

    [SerializeField]
    private float enemyVelocityIncreaseMultiplier = 1.1f;

    [SerializeField]
    private float verticalSpeed = .25f;

    [SerializeField]
    private float horizontalPositionMinLimit;

    [SerializeField]
    private float horizontalPositionMaxLimit;

    [SerializeField]
    private float verticalGameOverLimit;

    private float horizontalSpeed;

    public void StartMoving()
    {
        foreach (Enemy enemy in EnemyHordeController.Enemies)
        {
            enemy.Initialize(horizontalSpeed, horizontalPositionMinLimit, horizontalPositionMaxLimit, verticalGameOverLimit);
            enemy.OnReachBoundary += InvertSpeedAndMoveDown;
        }
    }

    public void IncreaseVelocity() 
    {
        Enemy.IncreaseSpeed(enemyVelocityIncreaseMultiplier);
    }

    public void StopMoving()
    {
        Enemy.ActivateMovement(false);

        foreach (Enemy enemy in EnemyHordeController.Enemies)
        {
            enemy.ActivateAnimation(false);
        }
    }

    public void InvertSpeedAndMoveDown()
    {
        Enemy.InvertSpeed();

        foreach (Enemy enemy in EnemyHordeController.Enemies)
        {
            enemy?.transform.Translate(Vector2.down * verticalSpeed);
        }
    }

    public void DefineMovementSpeed(int level)
    {
        horizontalSpeed = initialHorizontalSpeed * (level * enemyVelocityIncreaseMultiplier);
    }
}
