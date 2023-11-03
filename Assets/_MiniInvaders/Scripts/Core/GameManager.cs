using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    [SerializeField]
    private HUD hud;

    [SerializeField]
    private EnemyHordeController enemyHordeController;

    [SerializeField]
    private float verticalLimitToGameOver;

    [SerializeField]
    private float enemySpeed;

    private void Awake()
    {
        enemyHordeController.OnHordeSpawned += OnHordeSpawned;
        enemyHordeController.OnEnemyKilled += OnEnemyKilledHandler;
        enemyHordeController.OnLevelCompleted += OnLevelCompletedHandler;

        player.OnKilled += OnLostOneLive;

        InvaderController.OnReachEarth += OnLostOneLive;
    }

    private void Start()
    {
        enemyHordeController.Initialize();
    }
    private void OnHordeSpawned()
    {
        hud.OnAnimationEnd += OnHudAnimationEndHandler;
        hud.ShowStart();
    }

    private void OnEnemyKilledHandler(int KillPoints)
    {
        Globals.Score += KillPoints;
        hud.UpdateScore(Globals.Score);

        if (Globals.Score > Globals.HighScore)
        {
            Globals.HighScore = Globals.Score;
            hud.UpdateHiScore(Globals.HighScore);
        }
    }

    private void OnLostOneLive()
    {
        player.OnKilled -= OnLostOneLive;
        InvaderController.OnReachEarth -= OnLostOneLive;

        enemyHordeController.HordeMover.StopMoving();

        if(player != null)
            player.ActivateControl(false);

        Globals.lives--;

        if (hud == null)
            return;

        hud.OnAnimationEnd += OnHudAnimationEndHandler;

        if (Globals.lives <= 0)
        {
            Globals.level = 1;
            hud.ShowGameOver();
        }
        else
        {
            hud.LostOneLive();
        }
    }

    private void OnLevelCompletedHandler()
    {
        Globals.level++;
        hud.OnAnimationEnd += OnHudAnimationEndHandler;
        hud.ShowLevelComplete();
    }

    private void OnHudAnimationEndHandler(HUD.InfoType type)
    {
        hud.OnAnimationEnd -= OnHudAnimationEndHandler;

        switch (type)
        {
            case HUD.InfoType.Start:
                enemyHordeController.StartAttack();
                player.ActivateControl(true);
                break;

            case HUD.InfoType.LostOneLive:
                LoadScene("Game");
                break;


            case HUD.InfoType.LevelComplete:
                LoadScene("Game");
                break;

            case HUD.InfoType.GameOver:
                Globals.lives = 3;
                LoadScene("Menu");
                break;
        }
    }

    private void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
   
}