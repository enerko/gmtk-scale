using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip mainMenu;
    private AudioClip cave;
    private AudioClip forest;
    private AudioClip poacherWarehouse;

    void Start()
    {
        // May be changed to PlayMainMenuMusic later
        PlayCaveMusic();
    }

    // According to the scene change, change music with StageManager later
    public void PlayMainMenuMusic()
    {
        audioSource.clip = mainMenu;
        audioSource.Play();
    }

    public void PlayCaveMusic()
    {
        audioSource.clip = cave;
        audioSource.Play();
    }

    public void PlayForestMusic()
    {
        audioSource.clip = forest;
        audioSource.Play();
    }

    public void PlayPoacherWarehouseMusic()
    {
        audioSource.clip = poacherWarehouse;
        audioSource.Play();
    }
}
