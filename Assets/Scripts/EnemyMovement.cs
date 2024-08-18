using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2.0f;
    private bool isLeft = true;
    private float detectionRadius = 3.0f;
    private Transform player;
    private Vector2 initialChasePosition;
    private bool isPlayerInRange = false;

    // Find player object
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is in the detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            if (!isPlayerInRange)
            {
                // Save the position where the enemy starts chasing the player
                initialChasePosition = transform.position;
                isPlayerInRange = true;
            }

            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            if (isPlayerInRange)
            {
                // Return to the position where the enemy started chasing the player
                transform.position = Vector2.MoveTowards(transform.position, initialChasePosition, speed * Time.deltaTime);

                // If the enemy reaches the initial chase position, resume patrol
                if (Vector2.Distance(transform.position, initialChasePosition) < 0.1f)
                {
                    isPlayerInRange = false;
                }
            }
            else
            {
                // Default: move left
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
        }
    }

    // When hit the empty object with the tag "endPoint", turn right
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endPoint" && !isPlayerInRange)
        {
            if (isLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isLeft = true;
            }
        }
    }

    // Draw the detection range in the editor
    // This is for checking range mechanism. Can be deleted
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
