using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(InvadersMover))]
public class Invader : Enemy
{  
    public Action OnReachBoundary;
    public static Action OnReachEarth;

    [SerializeField]
    private EnemyBomb bullet;

    private InvadersMover movement;

    protected override void Start()
    {
        base.Start();
        movement = GetComponent<InvadersMover>();
    }
    
    public void Initialize(EnemiesSpeed speedController, EnemyPositionLimits enemyPositionLimits)
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
