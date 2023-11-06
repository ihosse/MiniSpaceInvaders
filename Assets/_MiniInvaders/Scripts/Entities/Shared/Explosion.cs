using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    public void Create(Vector2 position, Quaternion rotation)
    {
        Instantiate(prefab, position, rotation);
    }
}
