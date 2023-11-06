using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

[RequireComponent(typeof(UFOSpawner))]
[RequireComponent(typeof(InvadersSpawner))]
[RequireComponent(typeof(InvaderMover))]
[RequireComponent(typeof(InvadersShooter))]
public class EnemyHorde : MonoBehaviour
{
    public List<Invader> Enemies { get { return _enemies; } }
    public InvaderMover HordeMover { get { return enemyHordeMover; } }

    private List<Invader> _enemies;

    private UFOSpawner ufoSpawner;
    private InvadersSpawner enemyHordeSpawner;
    private InvaderMover enemyHordeMover;
    private InvadersShooter enemyShooter;

    public event Action OnHordeSpawned;
    public event Action OnLevelCompleted;
    public event Action<int> OnEnemyKilled;

    private int enemyKills;
    private int initialHordeSize;

    private EnemiesSpeed enemySpeedController;

    public void Initialize()
    {
        enemyKills = 0;

        enemySpeedController = GetComponent<EnemiesSpeed>();

        ufoSpawner = GetComponent<UFOSpawner>();
        enemyHordeSpawner = GetComponent<InvadersSpawner>();
        enemyHordeMover = GetComponent<InvaderMover>();
        enemyShooter = GetComponent<InvadersShooter>();

        ufoSpawner.Initialize(enemySpeedController);
        enemyHordeMover.Initialize(Globals.level, enemySpeedController);
        enemyShooter.DefineTimeToShot(Globals.level);

        enemyHordeSpawner.OnHordeSpawned += OnHordeSpawnedHandler;
        StartCoroutine(enemyHordeSpawner.CreateEnemyHorde());
    }

    private void OnHordeSpawnedHandler(List<Invader> enemies)
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

        foreach (Invader enemy in _enemies)
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
        _enemies.Remove(enemy as Invader);

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
