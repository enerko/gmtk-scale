using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static AudioSource SFX_source;

    public static void PlayClip(AudioClip clip)
    {
        SFX_source.PlayOneShot(clip);
    }
}
