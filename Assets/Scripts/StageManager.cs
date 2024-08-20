using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;

    private void Start()
    {
        if (audioManager == null)
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        PlayMusicForCurrentScene();
    }

    public void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 1")
        {
            SceneManager.LoadScene("Level 4");
        }
        /*else if (currentSceneName == "Forest")
        {
            SceneManager.LoadScene("PoacherWarehouse");
        }*/
        /*else if (currentSceneName == "PoacherWarehouse")
        {
            SceneManager.LoadScene("");  --- main menu or ending scene?
        }*/
    }


    private void PlayMusicForCurrentScene()
    {
        if (audioManager != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            audioManager.PlayMusicForScene(sceneName);
        }
    }

}
