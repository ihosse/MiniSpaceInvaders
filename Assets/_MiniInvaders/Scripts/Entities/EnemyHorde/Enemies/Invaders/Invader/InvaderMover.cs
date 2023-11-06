using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyPositionLimits
{
    public float horizontalMin;
    public float horizontalMax;
    public float verticalGameOver;
}

public class InvaderMover : MonoBehaviour 
{
    [SerializeField]
    private float enemyVelocityIncreaseMultiplier = 1.1f;

    [SerializeField]
    private float verticalSpeed = .25f;

    [SerializeField]
    private EnemyPositionLimits enemyPositionLimits;

    private List<Invader> enemies;
    private EnemiesSpeed enemySpeedController;

    private int currentLevel;

    public void Initialize(int currentLevel, EnemiesSpeed enemySpeedController)
    {
        this.currentLevel = currentLevel;
        this.enemySpeedController = enemySpeedController;
    }

    public void StartMoving(List <Invader> enemies)
    {
        this.enemies = enemies;
        enemySpeedController.DefineInitialSpeed(currentLevel);
        
        foreach (Invader enemy in enemies)
        {
            enemy.Initialize(enemySpeedController, enemyPositionLimits);
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

        foreach (Invader enemy in enemies)
        {
            enemy.ActivateAnimation(false);
        }
    }

    public void InvertSpeedAndMoveDown()
    {
        enemySpeedController.InvertSpeed();

        if (enemies == null)
            return;

        foreach (Invader enemy in enemies)
        {
            enemy?.transform.Translate(Vector2.down * verticalSpeed);
        }
    }
}
