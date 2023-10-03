using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float verticalMaxLimitPosition;

    [SerializeField]
    private float speed = 5;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(transform.position.y > verticalMaxLimitPosition) 
        {
            gameObject.SetActive(false);
            return;
        }

        transform.position += Vector3.up * Time.deltaTime * speed;
    }
}
