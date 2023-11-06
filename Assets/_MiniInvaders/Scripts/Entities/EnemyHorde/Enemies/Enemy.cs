using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Explosion))]
public abstract class Enemy : MonoBehaviour
{
    public int Points { get { return points; } }

    public event Action<Enemy> OnKill;

    [SerializeField]
    protected int points;

    protected Explosion explosion;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        explosion = GetComponent<Explosion>();
    }
    public virtual void TakeHit()
    {
        explosion.Create(transform.position, Quaternion.identity);
        OnKill?.Invoke(this);
    }

    public void ActivateAnimation(bool value)
    {
        if (animator != null)
            animator.SetBool("IsAnimating", value);
    }
}