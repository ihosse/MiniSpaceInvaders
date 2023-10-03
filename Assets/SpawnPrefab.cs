using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    public void Create(Vector2 position, Quaternion rotation)
    {
        Instantiate(prefab, position, rotation);
    }
}
