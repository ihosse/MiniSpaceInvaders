using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float verticalMaxLimitPosition;

    [SerializeField]
    private float verticalMinLimitPosition;

    [SerializeField]
    private bool destroyOndisable;

    [SerializeField]
    private float speed = 5;

    protected virtual void Update()
    {
        if (transform.position.y > verticalMaxLimitPosition)
        {
            Disable();
            return;
        }

        if (transform.position.y < verticalMinLimitPosition)
        {
            Disable();
            return;
        }

        transform.position += Vector3.up * Time.deltaTime * speed;
    }

    private void Disable()
    {
        if(destroyOndisable)
            Destroy(gameObject);
        else
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollisionHandler(collision);
    }

    protected virtual void CollisionHandler(Collider2D collision)
    {

    }
}
