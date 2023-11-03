using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpawnPrefab))]
[RequireComponent(typeof(InvaderMovement))]
public class InvaderController : MonoBehaviour
{
    public int Points { get { return points; } }
    
    public event Action<InvaderController> OnKill;
    public Action OnReachBoundary;
    public static Action OnReachEarth;

    [SerializeField]
    private int points;

    [SerializeField]
    private EnemyBomb bullet;

    private SpawnPrefab explosion;
    private Animator animator;

    private InvaderMovement movement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        explosion = GetComponent<SpawnPrefab>();
        movement = GetComponent<InvaderMovement>();
    }
    
    public void Initialize(EnemySpeedController speedController, EnemyPositionLimits enemyPositionLimits)
    {
        movement.Initialize(
                            this,
                            speedController,
                            enemyPositionLimits
                            );

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
}
