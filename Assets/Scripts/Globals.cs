using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{

    public static Globals Instance;
    private static int currLevel = 0;

    private void Awake()
    {
        // singleton since we need globals for each scene, undestroyed
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(Instance);
        }
    }

    public static void LoadNextLevel()
    {
        int nextLevel = currLevel + 1;
        SceneManager.LoadScene("Level " +  nextLevel);
        Debug.Log("loading" + "Level " + nextLevel);
    }

    public static void PlayClip(AudioClip clip)
    {
        Instance.GetComponent<AudioSource>().clip = clip;
        Instance.GetComponent<AudioSource>().Play();
    }
}
