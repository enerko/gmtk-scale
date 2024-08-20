using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Globals : MonoBehaviour
{

    public static Globals Instance;
    [SerializeField] private AudioClip _levelMusic;
    private static int _currLevel = 0;
    private AudioSource _audioSource;

    private void Awake()
    {
        // singleton since we need globals for each scene, undestroyed
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = GetComponent<AudioSource>();
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
        _currLevel += 1;
        SceneManager.LoadScene("Level " +  _currLevel);
        Debug.Log("loading" + "Level " + _currLevel);
    }

    public static void ReloadLevel()
    {
        SceneManager.LoadScene("Level " + _currLevel);
    }

    public static void PlayMusic(AudioClip music)
    {
        if (Instance._audioSource != null)
        {
            Instance._audioSource.clip = music;
            Instance._audioSource.Play();
            Instance._audioSource.loop = true;
        }
    }

    public static void PauseMusic()
    {
        if (Instance._audioSource != null)
        {
            Instance._audioSource.Stop();
            Instance._audioSource.clip = null;
        }
    }

    public static void PlaySFX(AudioClip sfx)
    {
        SFXManager.PlayClip(sfx);
    }
}
