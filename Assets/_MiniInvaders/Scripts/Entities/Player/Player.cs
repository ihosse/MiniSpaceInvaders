using System;
using UnityEngine;

[RequireComponent(typeof(Explosion))]
public class Player : MonoBehaviour
{
    public event Action OnKilled;

    [SerializeField]
    private Transform cannonTransform;

    [SerializeField]
    private float horizontalPositionMaxLimit;
    
    [SerializeField]
    private float horizontalPositionMinLimit;

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private PlayerBullet bullet;

    private Explosion explosion;
    private bool isInControl;

    private void Start()
    {
        explosion = GetComponent<Explosion>();
    }
    private void Update()
    {
        if (!isInControl)
            return;

        Move();
        LimitHorizontalMovement();
        Shot();
    }

    public void ActivateControl(bool value) 
    {
        isInControl = value;
    }

    private void Shot()
    {
        if (bullet.gameObject.activeSelf == true)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = cannonTransform.position;
        }
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector2(horizontalInput, 0);
        transform.position += direction * Time.deltaTime * speed;
    }

    private void LimitHorizontalMovement()
    {
        if (transform.position.x > horizontalPositionMaxLimit)
            transform.position = new Vector2(horizontalPositionMaxLimit, transform.position.y);
    
        if (transform.position.x < horizontalPositionMinLimit)
            transform.position = new Vector2(horizontalPositionMinLimit, transform.position.y);
    }

    public void TakeHit()
    {
        explosion.Create(transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        OnKilled?.Invoke();
    }
}
