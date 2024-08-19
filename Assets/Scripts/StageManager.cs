using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private AudioManager audioManager;
    private bool isReachedToExit = false;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        // Play music based on the current scene
        switch (SceneManager.GetActiveScene().name)
        {
            // Scene names
            case "MainMenu":
                audioManager.PlayMainMenuMusic();
                break;
            case "Cave":
                audioManager.PlayCaveMusic();
                break;
            case "Forest":
                audioManager.PlayForestMusic();
                break;
            case "PoacherWarehouse":
                audioManager.PlayPoacherWarehouseMusic();
                break;
            default:
                break;
        }
    }

    void Update()
    {
        // if player reaches to exit, change scene 
        if (!isReachedToExit)
        {
            LoadNextScene();
        }
    }

    public void LoadNextScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "MainMenu")
        {
            SceneManager.LoadScene("Cave");
        }
        else if (currentSceneName == "Cave")
        {
            SceneManager.LoadScene("Forest");
        }
        else if (currentSceneName == "Forest")
        {
            SceneManager.LoadScene("PoacherWarehouse");
        }
        else if (currentSceneName == "PoacherWarehouse")
        {
            // For the last scene, back to MainMenu or maybe another scene
            // SceneManager.LoadScene("MainMenu"); 
        }
    }
}