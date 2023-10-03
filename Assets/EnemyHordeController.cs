using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(EnemyHordeSpawner))]
[RequireComponent(typeof(EnemyHordeMover))]
[RequireComponent(typeof(EnemyShooter))]
public class EnemyHordeController : MonoBehaviour
{
    [SerializeField]
    private float enemySpeedMultiplier = 2;

    [SerializeField]
    private int[] numberOfKillsToIncreaseSpeed;
    public static List<Enemy> Enemies{ get { return _enemies; } }
    public EnemyHordeMover HordeMover { get { return enemyHordeMover; } }

    private static List<Enemy> _enemies;

    private EnemyHordeSpawner enemyHordeSpawner;
    private EnemyHordeMover enemyHordeMover;
    private EnemyShooter enemyShooter;

    public event Action OnHordeSpawned;
    public event Action OnLevelCompleted;
    public event Action<int> OnEnemyKilled;

    private int enemyKills;
    private int initialHordeSize;

    public void Initialize()
    {
        enemyKills = 0;

        enemyHordeSpawner = GetComponent<EnemyHordeSpawner>();
        enemyHordeMover = GetComponent<EnemyHordeMover>();
        enemyShooter = GetComponent<EnemyShooter>();

        enemyHordeMover.DefineMovementSpeed(Globals.level);
        enemyShooter.DefineTimeToShot(Globals.level);

        enemyHordeSpawner.OnHordeSpawned += OnHordeSpawnedHandler;
        StartCoroutine(enemyHordeSpawner.CreateEnemyHorde());
    }

    private void OnHordeSpawnedHandler(List<Enemy> enemies)
    {
        _enemies = enemies;
        initialHordeSize = _enemies.Count;

        SubscribeToEnemyKills();

        OnHordeSpawned?.Invoke();
    }

    public void StartAttack() 
    {
        enemyHordeMover.StartMoving();
        enemyShooter.StartShooting();
    }

    private void SubscribeToEnemyKills() 
    {
        foreach(Enemy enemy in _enemies) 
        {
            enemy.OnKill += OnKillHandler;
        }
    }

    private void OnKillHandler(Enemy enemy)
    {
        OnEnemyKilled?.Invoke(enemy.Points);

        Destroy(enemy.gameObject);
        _enemies.Remove(enemy);

        enemyKills++; 

        CheckEnemyVelocityIncrease();
        CheckIfLevelCompleted();
    }

    private void CheckIfLevelCompleted()
    {
        if(_enemies.Count <= 0) 
        {
            OnLevelCompleted?.Invoke();
        }
    }

    private void CheckEnemyVelocityIncrease()
    {
        foreach (var killNumberToIncreaseVelocity in numberOfKillsToIncreaseSpeed)
        {
            if(enemyKills == killNumberToIncreaseVelocity)
                Enemy.IncreaseSpeed(enemySpeedMultiplier);
        }
    }
}
