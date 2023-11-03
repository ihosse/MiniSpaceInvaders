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
    public List<InvaderController> Enemies{ get { return _enemies; } }
    public EnemyHordeMover HordeMover { get { return enemyHordeMover; } }

    private List<InvaderController> _enemies;

    private EnemyHordeSpawner enemyHordeSpawner;
    private EnemyHordeMover enemyHordeMover;
    private EnemyShooter enemyShooter;

    public event Action OnHordeSpawned;
    public event Action OnLevelCompleted;
    public event Action<int> OnEnemyKilled;

    private int enemyKills;
    private int initialHordeSize;

    private EnemySpeedController enemySpeedController;

    public void Initialize()
    {
        enemyKills = 0;

        enemySpeedController = new EnemySpeedController();

        enemyHordeSpawner = GetComponent<EnemyHordeSpawner>();
        enemyHordeMover = GetComponent<EnemyHordeMover>();
        enemyShooter = GetComponent<EnemyShooter>();

        enemyHordeMover.Initialize(Globals.level, enemySpeedController);
        enemyShooter.DefineTimeToShot(Globals.level);

        enemyHordeSpawner.OnHordeSpawned += OnHordeSpawnedHandler;
        StartCoroutine(enemyHordeSpawner.CreateEnemyHorde());
    }

    private void OnHordeSpawnedHandler(List<InvaderController> enemies)
    {
        _enemies = enemies;
        initialHordeSize = _enemies.Count;

        SubscribeToEnemyKills();

        OnHordeSpawned?.Invoke();
    }

    public void StartAttack() 
    {
        enemyHordeMover.StartMoving(_enemies);
        enemyShooter.StartShooting(_enemies);
    }

    private void SubscribeToEnemyKills() 
    {
        foreach(InvaderController enemy in _enemies) 
        {
            enemy.OnKill += OnKillHandler;
        }
    }

    private void OnKillHandler(InvaderController enemy)
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
                enemySpeedController.IncreaseSpeed(enemySpeedMultiplier);
        }
    }
}
