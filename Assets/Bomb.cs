using UnityEngine;
public class Bomb: MonoBehaviour
{
    [SerializeField]
    private float verticalMinLimitPosition;

    [SerializeField]
    private float speed = 5;

    private void Update()
    {
        if (transform.position.y < verticalMinLimitPosition)
        {
            Destroy(gameObject);
            return;
        }

        transform.position += speed * Time.deltaTime * Vector3.down;
    }
}