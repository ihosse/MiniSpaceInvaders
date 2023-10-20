using System;
using System.Collections.Generic;
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

    private List<Enemy> enemies;

    public void StartMoving(List <Enemy> enemies)
    {
        this.enemies = enemies;
        foreach (Enemy enemy in enemies)
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

        if (enemies == null)
            return;

        foreach (Enemy enemy in enemies)
        {
            enemy.ActivateAnimation(false);
        }
    }

    public void InvertSpeedAndMoveDown()
    {
        Enemy.InvertSpeed();

        if (enemies == null)
            return;

        foreach (Enemy enemy in enemies)
        {
            enemy?.transform.Translate(Vector2.down * verticalSpeed);
        }
    }

    public void DefineMovementSpeed(int level)
    {
        horizontalSpeed = initialHorizontalSpeed * (level * enemyVelocityIncreaseMultiplier);
    }
}
