using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class UpdateMenuInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI score;

    [SerializeField]
    private TextMeshProUGUI hiScore;

    private void Start()
    {
        score.text = "SCORE:\n" + 0.ToString("D5");
        hiScore.text = "HI-SCORE:\n" + Globals.HighScore.ToString("D5");
    }
}
