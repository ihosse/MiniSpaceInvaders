using UnityEngine;

public class UFO : Enemy
{
    [SerializeField]
    private Vector2 initialPosition;

    [SerializeField]
    private float speedX = 5;

    [SerializeField]
    private float horizontalFinalPosition;

    private EnemiesSpeed speedController;

    public void OnEnable()
    {
        transform.position = initialPosition;
    }

    public void Initialize(EnemiesSpeed speedController)
    {
        this.speedController = speedController;
        ActivateAnimation(true);
    }

    private void Update()
    {
        if (speedController == null)
            return;

        if (!speedController.IsMoving)
            return;

        transform.Translate(Vector2.left * Time.deltaTime * speedX);

        CheckBoundaryReached();
    }

    private void CheckBoundaryReached()
    {
        if (transform.position.x < horizontalFinalPosition)
        {
            gameObject.SetActive(false);
        }
    }
}
