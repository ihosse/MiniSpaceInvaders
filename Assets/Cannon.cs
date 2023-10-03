using System;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    private bool isReadyToShot;

    public void ActivateControl(bool value)
    {
        isReadyToShot = value;
    }

    void Update()  
    {
        if (bullet.gameObject.activeSelf == true || isReadyToShot == false)
            return;

        if (Input.GetButton("Fire1"))
        {
            bullet.gameObject.SetActive(true);
            bullet.transform.position = transform.position;
        }
    }
}
