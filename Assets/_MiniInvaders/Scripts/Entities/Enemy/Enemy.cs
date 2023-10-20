using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnPrefab))]
public class Enemy : MonoBehaviour
{
    public static float Speed { get; private set; }
    
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

    private static bool isMoving;

    private float horizontalPositionMinLimit;
    private float horizontalPositionMaxLimit;
    private float verticalGameOverLimit;

    private void Start()
    {
        animator = GetComponent<Animator>();
        explosion = GetComponent<SpawnPrefab>();
        ActivateMovement(false);
    }
    
    private void Update()
    {
        if (!isMoving)
            return;

        transform.Translate(Vector2.right * Time.deltaTime * Speed);

        CheckBoundaryReached();
        CheckVerticalLimit();
    }

    public void TakeHit()
    {
        explosion.Create(transform.position, Quaternion.identity);
        OnKill?.Invoke(this);
    }

    public void Initialize(float initialSpeed, float horizontalPositionMinLimit, float horizontalPositionMaxLimit, float verticalGameOverLimit)
    {
        Speed = initialSpeed;
        this.horizontalPositionMinLimit = horizontalPositionMinLimit;
        this.horizontalPositionMaxLimit = horizontalPositionMaxLimit;
        this.verticalGameOverLimit = verticalGameOverLimit;

        ActivateAnimation(true);
        ActivateMovement(true);
    }
    public void Shot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    public static void IncreaseSpeed(float multiplier) 
    {
        Speed *= multiplier;
    }

    public static void ActivateMovement(bool value) 
    {
        isMoving = value;
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

    internal static void InvertSpeed()
    {
        Speed *= -1;
    }
}
