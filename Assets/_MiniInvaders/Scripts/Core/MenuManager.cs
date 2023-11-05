using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI hiScore;

    private void Start()
    {
        Globals.Score = 0;
        score.text = "SCORE:\n" + Globals.Score.ToString("D5");
        hiScore.text = "HI-SCORE:\n" + Globals.HighScore.ToString("D5");
    }
}
