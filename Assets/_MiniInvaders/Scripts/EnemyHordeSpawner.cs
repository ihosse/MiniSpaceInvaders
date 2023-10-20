using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

class EnemyHordeSpawner : MonoBehaviour
{
    public event Action<List<Enemy>> OnHordeSpawned;

    [SerializeField]
    private int horizontalSize = 11;

    [SerializeField]
    private int verticalSize = 5;

    [SerializeField]
    private float verticalOffset = 0;

    [SerializeField]
    private float spaceBetweenEnemies = 1;

    [SerializeField]
    private GameObject[] enemiesPrefab;

    [SerializeField]
    private int maxEnemyRolls = 2;
    private int currentEnemyRolls;

    public IEnumerator CreateEnemyHorde()
    {
        int enemyIndex = 0;
        List<Enemy> enemies = new List<Enemy>();

        for (int row = 0; row < verticalSize; row++)
        {
            for (int col = 0; col < horizontalSize; col++)
            {
                float horizontalOffset = (horizontalSize / 2) * spaceBetweenEnemies;

                float horizontalPosition = (col * spaceBetweenEnemies) - horizontalOffset;
                float verticalPosition = (row * spaceBetweenEnemies) - verticalOffset;

                var currentEnemy = Instantiate(enemiesPrefab[enemyIndex], this.transform).GetComponent<Enemy>();
                currentEnemy.transform.position = new Vector2(horizontalPosition, verticalPosition);

                enemies.Add(currentEnemy);

                yield return new WaitForSeconds(.025f);
            }
            enemyIndex = ReturnNewIndexIfRequired(enemyIndex);
        }
        OnHordeSpawned?.Invoke(enemies);
    }

    private int ReturnNewIndexIfRequired(int currentIndex)
    {
        int newIndex = currentIndex;
        currentEnemyRolls++;

        if (currentEnemyRolls >= maxEnemyRolls)
        {
            currentEnemyRolls = 0;
            newIndex++;

            if (newIndex >= enemiesPrefab.Length)
                newIndex = 0;
        }
        return newIndex;
    }
}
