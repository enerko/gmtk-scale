using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private GameObject player;
    private int respawnIndex = 0;
    // Delay time
    private float delayBeforeRespawn = 1f;
    private HashSet<Collider2D> changedSpawnPoints = new HashSet<Collider2D>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(PlayerRespawn());
            Debug.Log("Current respawn index: " + respawnIndex);
        }
        if (other.CompareTag("ChangeSpawnPoint") && !changedSpawnPoints.Contains(other))
        {
            // Increase respawn index with keeping the last respawn point
            if (respawnIndex < respawnPoints.Length - 1)
            {
                respawnIndex++;
            }
            // Add changed spawn point to the list not to increase index again
            changedSpawnPoints.Add(other); 
            Debug.Log("Changed spawn point to index: " + respawnIndex);
        }
    }

    IEnumerator PlayerRespawn()
    {
        // Wait enemy's movement finish
        yield return new WaitForSeconds(delayBeforeRespawn);

        // Move player to the respawn point
        player.transform.position = respawnPoints[respawnIndex].position;
    }
}