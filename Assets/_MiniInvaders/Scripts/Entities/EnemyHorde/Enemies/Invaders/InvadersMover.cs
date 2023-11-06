using UnityEngine;

public class InvadersMover : MonoBehaviour
{
    private float horizontalPositionMinLimit;
    private float horizontalPositionMaxLimit;
    private float verticalGameOverLimit;

    private Invader invader;
    private EnemiesSpeed speedController;

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

    public void Initialize(Invader invader, EnemiesSpeed speedController, EnemyPositionLimits enemyPositionLimits)
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
            Invader.OnReachEarth?.Invoke();
    }
}
