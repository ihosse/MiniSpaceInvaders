using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField]
    private string inputButtonName;

    [SerializeField]
    private string sceneName;
    void Update()
    {
        if (Input.GetButtonDown(inputButtonName))
            SceneManager.LoadScene(sceneName);
    }
}
