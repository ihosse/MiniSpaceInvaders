using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

[RequireComponent(typeof(UFOSpawner))]
[RequireComponent(typeof(InvaderHordeSpawner))]
[RequireComponent(typeof(InvaderHordeMover))]
[RequireComponent(typeof(InvaderHordeShooter))]
public class EnemyHordeController : MonoBehaviour
{
    public List<InvaderController> Enemies { get { return _enemies; } }
    public InvaderHordeMover HordeMover { get { return enemyHordeMover; } }

    private List<InvaderController> _enemies;

    private UFOSpawner ufoSpawner;
    private InvaderHordeSpawner enemyHordeSpawner;
    private InvaderHordeMover enemyHordeMover;
    private InvaderHordeShooter enemyShooter;

    public event Action OnHordeSpawned;
    public event Action OnLevelCompleted;
    public event Action<int> OnEnemyKilled;

    private int enemyKills;
    private int initialHordeSize;

    private EnemySpeedController enemySpeedController;

    public void Initialize()
    {
        enemyKills = 0;

        enemySpeedController = GetComponent<EnemySpeedController>();

        ufoSpawner = GetComponent<UFOSpawner>();
        enemyHordeSpawner = GetComponent<InvaderHordeSpawner>();
        enemyHordeMover = GetComponent<InvaderHordeMover>();
        enemyShooter = GetComponent<InvaderHordeShooter>();

        ufoSpawner.Initialize(enemySpeedController);
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
        ufoSpawner.Ufo.OnKill += OnUFOKillHander;

        foreach (InvaderController enemy in _enemies)
        {
            enemy.OnKill += OnInvaderKillHandler;
        }
    }

    private void OnUFOKillHander(Enemy enemy)
    {
        OnEnemyKilled?.Invoke(enemy.Points);
        enemy.gameObject.SetActive(false);
    }

    private void OnInvaderKillHandler(Enemy enemy)
    {
        OnEnemyKilled?.Invoke(enemy.Points);

        Destroy(enemy.gameObject);
        _enemies.Remove(enemy as InvaderController);

        enemyKills++;

        CheckEnemyVelocityIncrease();
        CheckIfLevelCompleted();
    }

    private void CheckIfLevelCompleted()
    {
        if (_enemies.Count <= 0)
        {
            OnLevelCompleted?.Invoke();
        }
    }

    private void CheckEnemyVelocityIncrease()
    {
        float percentOfEnemyKilled = (float)enemyKills / (float)initialHordeSize;
        enemySpeedController.IncreaseSpeed(percentOfEnemyKilled);
    }
}
