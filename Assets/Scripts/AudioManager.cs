using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    //[SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip cave;
    [SerializeField] private AudioClip forest;
    //[SerializeField] private AudioClip poacherWarehouse;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayMusicForScene(string sceneName)
    {
        AudioClip clip = null;

        switch (sceneName)
        {
            /*case "MainMenu":
                clip = mainMenu;
                break;*/
            case "Cave":
                clip = cave;
                break;
            case "Forest":
                clip = forest;
                break;
            /*case "PoacherWarehouse":
                clip = poacherWarehouse;
                break;*/
                // Add more scenes if needed
        }

        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
