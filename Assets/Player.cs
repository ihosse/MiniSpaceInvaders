using System;
using UnityEngine;

[RequireComponent(typeof(SpawnPrefab))]
public class Player : MonoBehaviour, ITakeHits
{
    public event Action OnKilled;

    [SerializeField]
    private float horizontalPositionMaxLimit;
    
    [SerializeField]
    private float horizontalPositionMinLimit;

    [SerializeField]
    private float speed = 5;

    [SerializeField]
    private PlayerBullet bullet;

    private SpawnPrefab explosion;
    private bool isInControl;

    public void ActivateControl(bool value) 
    {
        explosion = GetComponent<SpawnPrefab>();
        isInControl = value;
    }

    private void Update()
    {
        if (!isInControl)
            return;

        Move();
        LimitHorizontalMovement();
        Shot();
    }

    private void Shot()
    {
        if (bullet.gameObject.activeSelf == true)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
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
