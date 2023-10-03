using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter:MonoBehaviour 
{
    [SerializeField]
    private float initialTimeToShoot = 1;

    [SerializeField]
    private float timetoShootDecreaseFactor = .1f;

    private float timeToShoot;

    private IEnumerator coroutine;

    public void DefineTimeToShot(int level) 
    {
        timeToShoot = initialTimeToShoot / (level * timetoShootDecreaseFactor);
    }
    public void StartShooting()
    {
        coroutine = ChooseEnemyAndShot();
        StartCoroutine(coroutine);
    }

    private IEnumerator ChooseEnemyAndShot() 
    {
        while (true)
        {
            if (EnemyHordeController.Enemies.Count <= 0)
                break;

            int randomIndex = Random.Range(0, EnemyHordeController.Enemies.Count);
            EnemyHordeController.Enemies[randomIndex].Shot();

            yield return new WaitForSeconds(timeToShoot);
        }
    }
}
