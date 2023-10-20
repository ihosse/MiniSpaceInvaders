using UnityEngine;
public class DestroyTimer: MonoBehaviour
{
    [SerializeField]
    private float secondsToDestroy;

    private void Start()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}