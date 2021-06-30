using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private int _menuSceneIndex = 0;
    private int _currentSceneIndex;

    public void TryLoadNextScene()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (_currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(++_currentSceneIndex);
        }
    }

    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(_menuSceneIndex);
    }

}
