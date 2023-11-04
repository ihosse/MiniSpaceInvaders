using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InvaderMovement))]
public class InvaderController : Enemy
{  
    public Action OnReachBoundary;
    public static Action OnReachEarth;

    [SerializeField]
    private EnemyBomb bullet;

    private InvaderMovement movement;

    protected override void Start()
    {
        base.Start();
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
    
    public void Shot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

    
}
