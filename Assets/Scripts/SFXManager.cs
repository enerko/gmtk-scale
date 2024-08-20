using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private static AudioSource SFX_source;

    private void Awake()
    {
        // Initialize the AudioSource if it hasn't been initialized yet
        if (SFX_source == null)
        {
            SFX_source = GetComponent<AudioSource>();
            if (SFX_source == null)
            {
                // Add an AudioSource component if it does not exist
                SFX_source = gameObject.AddComponent<AudioSource>();
            }
        }
    }

    public static void PlayClip(AudioClip clip)
    {
        if (SFX_source == null)
        {
            Debug.LogError("SFX_source is not initialized.");
            return;
        }
        SFX_source.PlayOneShot(clip);
    }

    public static void AdjustVolume(float volume)
    {
        SFX_source.volume = volume;
    }
}
