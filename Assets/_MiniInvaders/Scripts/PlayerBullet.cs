using UnityEngine;

public class PlayerBullet : Projectile
{
    protected override void CollisionHandler(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            gameObject.SetActive(false);
            enemy.TakeHit();
        }
    }
}