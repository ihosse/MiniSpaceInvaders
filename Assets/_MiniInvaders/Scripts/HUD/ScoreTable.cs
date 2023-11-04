using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField]
    private GameObject scoreTableObj;

    [SerializeField]
    private GameObject infoPrefab;

    [SerializeField]
    private Enemy[] enemies;

    private void Start() 
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject info = Instantiate(infoPrefab, scoreTableObj.transform);
            info.GetComponent<RectTransform>().localPosition = new Vector2(0, (i * 60) - 50);

            TextMeshProUGUI pointsinfo = info.GetComponentInChildren<TextMeshProUGUI>();
            Image imageInfo = info.GetComponentInChildren<Image>();

            pointsinfo.text = "= " + enemies[i].Points + " POINTS";
            imageInfo.sprite = enemies[i].GetComponent<SpriteRenderer>().sprite;
        }
        
    } 

}
