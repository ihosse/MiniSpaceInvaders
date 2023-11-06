using System.Collections;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    public UFO Ufo { get; private set; }

    [SerializeField]
    private float minTimeToShow;

    [SerializeField]
    private float maxTimeToShow;

    [SerializeField]
    private UFO ufoPrefab;
    
    private float timeToShow;

    public void Initialize(EnemySpeedController speedController)
    {
        Ufo = Instantiate(ufoPrefab);
        Ufo.Initialize(speedController);

        StartCoroutine(ShowUFO());
    }

    private IEnumerator ShowUFO() 
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToShow);

            timeToShow = Random.Range(minTimeToShow, maxTimeToShow);
            Ufo.gameObject.SetActive(true);
        }
    }
}