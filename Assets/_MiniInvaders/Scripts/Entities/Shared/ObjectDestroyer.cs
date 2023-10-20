using UnityEngine;
public class ObjectDestroyer: MonoBehaviour
{
    [SerializeField]
    private float secondsToDestroy;

    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}