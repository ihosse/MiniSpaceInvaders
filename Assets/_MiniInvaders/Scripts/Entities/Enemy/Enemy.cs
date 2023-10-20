using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnPrefab))]
public class Enemy : MonoBehaviour
{
    public int Points { get { return points; } }
    
    public event Action<Enemy> OnKill;
    public event Action OnReachBoundary;
    public static event Action OnReachEarth;

    [SerializeField]
    private int points;

    [SerializeField]
    private EnemyBomb bullet;

    private SpawnPrefab explosion;
    private Animator animator;

    private float horizontalPositionMinLimit;
    private float horizontalPositionMaxLimit;
    private float verticalGameOverLimit;

    private EnemySpeedController speedController;

    private void Start()
    {
        animator = GetComponent<Animator>();
        explosion = GetComponent<SpawnPrefab>();
    }
    
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
    public void Initialize(EnemySpeedController speedController, float horizontalPositionMinLimit, float horizontalPositionMaxLimit, float verticalGameOverLimit)
    {
        this.speedController = speedController;
        this.horizontalPositionMinLimit = horizontalPositionMinLimit;
        this.horizontalPositionMaxLimit = horizontalPositionMaxLimit;
        this.verticalGameOverLimit = verticalGameOverLimit;

        ActivateAnimation(true);
    }

    public void TakeHit()
    {
        explosion.Create(transform.position, Quaternion.identity);
        OnKill?.Invoke(this);
    }

    
    public void Shot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    public void ActivateAnimation(bool value) 
    {
        animator.SetBool("IsAnimating", value);
    }

    private void CheckBoundaryReached()
    {
        if (transform.position.x > horizontalPositionMaxLimit)
        {
            transform.position = new Vector2(horizontalPositionMaxLimit, transform.position.y);
            OnReachBoundary?.Invoke();
        }

        if (transform.position.x < horizontalPositionMinLimit)
        {
            transform.position = new Vector2(horizontalPositionMinLimit, transform.position.y);
            OnReachBoundary?.Invoke();
        }
    }

    private void CheckVerticalLimit() 
    {
        if(transform.position.y < verticalGameOverLimit) 
            OnReachEarth?.Invoke();
    }
}
