using UnityEngine;
public class EnemyBomb: Projectile
{
    protected override void CollisionHandler(Collider2D collision)
    {
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            Destroy(collision.gameObject);
            player.TakeHit();
        }
    }
}