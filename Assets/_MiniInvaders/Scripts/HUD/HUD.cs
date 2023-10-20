using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class HUD:MonoBehaviour
{
    public enum InfoType { Start, LostOneLive, GameOver, LevelComplete }
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI hiScore;

    [SerializeField]
    private TextMeshProUGUI centerLabel;

    [SerializeField]
    private TextMeshProUGUI upperLabel;

    [SerializeField]
    private TextMeshProUGUI lives;

    public event Action <InfoType> OnAnimationEnd;

    private void Start()
    {
        centerLabel.gameObject.SetActive(false);
        upperLabel.gameObject.SetActive(false);

        UpdateScore(0);
        UpdateHiScore(Globals.HighScore);
        UpdateLives(Globals.lives);
    }

    public void UpdateScore(int value) 
    {
        score.text = "SCORE:\n" + value.ToString("D5");
    }

    public void UpdateLives(int value) 
    {
        lives.text = "LIVES: " + value;
    }

    public void UpdateHiScore(int value) 
    {
        hiScore.text = "HI-SCORE:\n" + value.ToString("D5");
    }

    public void ShowStart() 
    {
        string message = "LEVEL " + Globals.level + "\nSTART!";
        StartCoroutine(ShowInfo(centerLabel, message, InfoType.Start));
    }
    public void LostOneLive()
    {
        string message = "YOU LOST!";
        StartCoroutine(ShowInfo(upperLabel, message, InfoType.LostOneLive));
    }

    public void ShowGameOver()
    {
        string message = "GAME OVER!";
        StartCoroutine(ShowInfo(upperLabel, message, InfoType.GameOver));
    }

    public void ShowLevelComplete()
    {
        string message = "LEVEL COMPLETE!";
        StartCoroutine(ShowInfo(centerLabel, message, InfoType.LevelComplete));
    }

    private IEnumerator ShowInfo(TextMeshProUGUI label, string message, InfoType type) 
    {
        label.text = message;

        for (int i = 0; i < 3; i++)
        {
            label.gameObject.SetActive(true);
            yield return new WaitForSeconds(.25f);
            label.gameObject.SetActive(false);
            yield return new WaitForSeconds(.25f);
        }
        OnAnimationEnd?.Invoke(type);
    }
}