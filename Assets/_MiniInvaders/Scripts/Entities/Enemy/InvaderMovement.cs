using UnityEngine;

public class InvaderMovement : MonoBehaviour
{
    private float horizontalPositionMinLimit;
    private float horizontalPositionMaxLimit;
    private float verticalGameOverLimit;

    private InvaderController invader;
    private EnemySpeedController speedController;

    private void Update()
    {
        if (speedController == null)
            return;

        if (!speedController.IsMoving)
            return;

        transform.Translate(Vector2.right * Time.deltaTime * speedController.Speed);

        CheckBoundaryReached();
        CheckVerticalLimit();
    }

    public void Initialize(InvaderController invader, EnemySpeedController speedController, EnemyPositionLimits enemyPositionLimits)
    {
        this.invader = invader;
        this.speedController = speedController;

        horizontalPositionMinLimit = enemyPositionLimits.horizontalMin;
        horizontalPositionMaxLimit = enemyPositionLimits.horizontalMax;
        verticalGameOverLimit = enemyPositionLimits.verticalGameOver;
    }

    private void CheckBoundaryReached()
    {
        if (transform.position.x > horizontalPositionMaxLimit)
        {
            transform.position = new Vector2(horizontalPositionMaxLimit, transform.position.y);
            invader.OnReachBoundary?.Invoke();
        }

        if (transform.position.x < horizontalPositionMinLimit)
        {
            transform.position = new Vector2(horizontalPositionMinLimit, transform.position.y);
            invader.OnReachBoundary?.Invoke();
        }
    }

    private void CheckVerticalLimit()
    {
        if (transform.position.y < verticalGameOverLimit)
            InvaderController.OnReachEarth?.Invoke();
    }
}
