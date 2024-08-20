using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{

    public static Globals Instance;
    [SerializeField] private AudioClip _levelMusic;
    private static int _currLevel = 0;

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

    private void Start()
    {
        PlayMusic(_levelMusic);
    }

    public static void LoadNextLevel()
    {
        int nextLevel = _currLevel + 1;
        SceneManager.LoadScene("Level " +  nextLevel);
        Debug.Log("loading" + "Level " + nextLevel);
    }

    public static void PlayMusic(AudioClip music)
    {
        Instance.GetComponent<AudioSource>().clip = music;
        Instance.GetComponent<AudioSource>().Play();
        Instance.GetComponent<AudioSource>().loop = true;   
    }

    public static void PlaySFX(AudioClip sfx)
    {
        SFXManager.PlayClip(sfx);
    }
}
