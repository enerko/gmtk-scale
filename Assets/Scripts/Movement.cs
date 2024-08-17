using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _horizontal = 0;
    private float _vertical = 0;
    private float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private bool _isClimbing;
    private float _rotationSpeed = 1f;

    public Vector2 boxSize = new Vector2(2,2);
    public float groundCastDistance = 10f;
    public float wallCastDistance = 10f;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        _horizontal = Input.GetAxisRaw("Horizontal");

        // If the player is next to a wall, rotate them 
        if (IsNextToAWall())
        {
            Debug.Log("Is next to a wall");
        }


        // If player is next to a wall, pressing W should let them rotate then walk
        if (Input.GetKeyDown(KeyCode.W) && !_isClimbing)
        {
            // the way the player is facing
            Vector2 direction = Vector2.right * Mathf.Sign(Input.GetAxis("Horizontal"));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            Debug.Log(hit);

            if (hit.collider != null && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                StartClimbing(hit.normal);
            }
        }

        // Handle climbing movement if currently climbing
        else if (_isClimbing)
        {
            _vertical = Input.GetAxis("Vertical");
        }
    }

    private bool IsGrounded()
    {
        // Check that the player is grounded 
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCastDistance, groundLayer))
        {
            return true;
        }
        else 
        { 
            return false; 
        }
    }

    private bool IsNextToAWall()
    {
        // Check that the player is next to and facing a wall
        if (Physics2D.BoxCast(transform.position, boxSize, 0, transform.forward, wallCastDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FixedUpdate()
    {
        // Move player only if grounded
        if (IsGrounded())
        {
            _rb.velocity = new Vector2(_horizontal * _moveSpeed, _vertical * _moveSpeed);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize raycast 
        Gizmos.DrawWireCube(transform.position - transform.forward * wallCastDistance, boxSize);
    }

    void StartClimbing(Vector2 wallNormal)
    {
        _isClimbing = true;

        // Rotate the player to face the wall
        float angle = Mathf.Atan2(wallNormal.y, wallNormal.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), _rotationSpeed * Time.deltaTime);
    }

    void ClimbWall()
    {
        // Move up or down the wall with W and S keys
        _vertical = Input.GetAxis("Vertical");

    }
}
