using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EnemyPositionLimits
{
    public float horizontalMin;
    public float horizontalMax;
    public float verticalGameOver;
}

public class InvaderHordeMover : MonoBehaviour 
{
    [SerializeField]
    private float enemyVelocityIncreaseMultiplier = 1.1f;

    [SerializeField]
    private float verticalSpeed = .25f;

    [SerializeField]
    private EnemyPositionLimits enemyPositionLimits;

    private List<InvaderController> enemies;
    private EnemySpeedController enemySpeedController;

    public void Initialize(int level, EnemySpeedController enemySpeedController)
    {
        this.enemySpeedController = enemySpeedController;
    }

    public void StartMoving(List <InvaderController> enemies)
    {
        this.enemies = enemies;
        enemySpeedController.DefineInitialSpeed();
        
        foreach (InvaderController enemy in enemies)
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

        foreach (InvaderController enemy in enemies)
        {
            enemy.ActivateAnimation(false);
        }
    }

    public void InvertSpeedAndMoveDown()
    {
        enemySpeedController.InvertSpeed();

        if (enemies == null)
            return;

        foreach (InvaderController enemy in enemies)
        {
            enemy?.transform.Translate(Vector2.down * verticalSpeed);
        }
    }
}
