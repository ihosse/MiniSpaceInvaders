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
    private EnemySpeedController enemySpeedController;

    public void Initialize(int level, EnemySpeedController enemySpeedController)
    {
        this.enemySpeedController = enemySpeedController;
        horizontalSpeed = initialHorizontalSpeed * (level * enemyVelocityIncreaseMultiplier);
    }

    public void StartMoving(List <Enemy> enemies)
    {
        this.enemies = enemies;
        enemySpeedController.DefineInitialSpeed(horizontalSpeed);
        
        foreach (Enemy enemy in enemies)
        {
            enemy.Initialize(enemySpeedController, horizontalPositionMinLimit, horizontalPositionMaxLimit, verticalGameOverLimit);
            enemy.OnReachBoundary += InvertSpeedAndMoveDown;
        }

        enemySpeedController.ActivateMovement(true);
    }

    public void IncreaseVelocity() 
    {
        enemySpeedController.IncreaseSpeed(enemyVelocityIncreaseMultiplier);
    }

    public void StopMoving()
    {
        enemySpeedController.ActivateMovement(false);

        if (enemies == null)
            return;

        foreach (Enemy enemy in enemies)
        {
            enemy.ActivateAnimation(false);
        }
    }

    public void InvertSpeedAndMoveDown()
    {
        enemySpeedController.InvertSpeed();

        if (enemies == null)
            return;

        foreach (Enemy enemy in enemies)
        {
            enemy?.transform.Translate(Vector2.down * verticalSpeed);
        }
    }

    
}
