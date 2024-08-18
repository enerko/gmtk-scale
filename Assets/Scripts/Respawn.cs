using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint; 
    [SerializeField] private GameObject player;
    // Delay time
    private float delayBeforeRespawn = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(RespawnPlayerAfterDelay());
        }
    }

    IEnumerator RespawnPlayerAfterDelay()
    {
        // Wait enemy's movement finish
        yield return new WaitForSeconds(delayBeforeRespawn);

        // Move player to the respawn point
        player.transform.position = respawnPoint.position;
    }
}