using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2.0f;
    private bool isLeft = true;
    private float detectionRadius = 3.0f;
    private Transform player;

    // Find player object
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    void Update()
    {
        // Check if the player is in the detect range
        if (Vector2.Distance(transform.position, player.position) <= detectionRadius)
        {
            // Move to the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            // Default: move left
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }

    // When hit the empty object with the tag "endPoint", turn right
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "endPoint")
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
