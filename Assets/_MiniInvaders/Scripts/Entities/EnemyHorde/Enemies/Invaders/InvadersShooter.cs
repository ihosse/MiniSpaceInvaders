using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvadersShooter:MonoBehaviour 
{
    [SerializeField]
    private float initialTimeToShoot = 1;

    [SerializeField]
    private float timetoShootDecreaseFactor = .1f;

    private float timeToShoot;

    private IEnumerator coroutine;

    private List<Invader> enemies;

    public void DefineTimeToShot(int level) 
    {
        timeToShoot = initialTimeToShoot / (level * timetoShootDecreaseFactor);
    }
    public void StartShooting(List<Invader> enemies)
    {
        this.enemies = enemies;
        coroutine = ChooseEnemyAndShot();
        StartCoroutine(coroutine);
    }

    private IEnumerator ChooseEnemyAndShot() 
    {
        while (true)
        {
            if (enemies.Count <= 0)
                break;

            int randomIndex = Random.Range(0, enemies.Count);
            enemies[randomIndex].Shot();

            yield return new WaitForSeconds(timeToShoot);
        }
    }
}
