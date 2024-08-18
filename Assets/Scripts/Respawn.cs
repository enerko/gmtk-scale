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
    // Default respawn face-direction is right
    private Vector3 respawnDirection = Vector3.right; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            StartCoroutine(PlayerRespawn());
            Debug.Log("Current respawn index: " + respawnIndex);
        }
        if (other.CompareTag("ChangeSpawnPoint") && !changedSpawnPoints.Contains(other))
        {
            // Determine the player's movement direction
            if (player.transform.position.x < other.transform.position.x)
            {
                respawnDirection = Vector3.right; // Moving right
            }
            else
            {
                respawnDirection = Vector3.left; // Moving left
            }

            // Increase respawn index while keeping the last respawn point
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

        // Set the player's facing direction based on respawnDirection
        if (respawnDirection == Vector3.right)
        {
            player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            Debug.Log("Player is facing right after respawn.");
        }
        else if (respawnDirection == Vector3.left)
        {
            player.transform.localScale = new Vector3(-Mathf.Abs(player.transform.localScale.x), player.transform.localScale.y, player.transform.localScale.z);
            Debug.Log("Player is facing left after respawn.");
        }
    }
}
