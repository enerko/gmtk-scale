using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontal;
    private float _vertical;
    private float _moveSpeed = 5f;
    private int _direction = -1;



    [SerializeField] private Vector2 _boxSize = new Vector2(1f, 2f);
    [SerializeField] private Vector2 _offset = new Vector2(1f, 1f);
    [SerializeField] private float _groundCastDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _cornerLayer;
    [SerializeField] private float _downwardForce;
    [SerializeField] private float _angleTolerance = 20f;
    [SerializeField] private float _wallBoxSize;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get horizontal and vertical input
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (!IsTouching(_cornerLayer))
        {
            CheckDirection();
            if (_horizontal != 0 || _vertical != 0)
            {
                Flip();
            }
            
        }
        

        if(!IsTouching(_groundLayer))
        {
            _rb.velocity += (Vector2)(-transform.up) * _downwardForce;
        }
        _rb.velocity = new Vector2(_horizontal * _moveSpeed, _vertical * _moveSpeed);

    }

    private void CheckDirection()
    {
        if (!IsTouching(_cornerLayer))
        {
            // Calculate the angle between the player's up vector and the world axes
            float angleUp = Vector2.Angle(transform.up, Vector2.up);
            float angleRight = Vector2.Angle(transform.up, Vector2.right);

            // Check if the player's up vector is close to vertical or horizontal
            if (Mathf.Abs(angleUp) < _angleTolerance || Mathf.Abs(angleUp - 180f) < _angleTolerance)
            {
                // Player is facing mostly up or down
                _vertical = 0;
            }
            else if (Mathf.Abs(angleRight) < _angleTolerance || Mathf.Abs(angleRight - 90f) < _angleTolerance)
            {
                // Player is facing mostly right or left
                _horizontal = 0;
            }
        }
    }

    private void Flip()
    {
        Vector2 localScale = transform.localScale;
        Vector2 inputVector = new Vector2(_horizontal, _vertical);

        Debug.Log("input" + inputVector);
        Debug.Log(localScale);
        if (inputVector.normalized == -1 * localScale.normalized)
        {
            localScale.x *= -1;
        }

        // Apply the updated scale
        transform.localScale = localScale;
    }

    private bool IsTouching(LayerMask layer)
    {
        // Calculate the rotated offset
        Vector3 rotatedOffset = transform.rotation * _offset;

        // Check if the player is grounded
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + rotatedOffset, _boxSize, transform.eulerAngles.z, -transform.up, _groundCastDistance, layer);
        return hit.collider != null;
    }



    private void OnDrawGizmos()
    {
        // Calculate the rotated offset
        Vector3 rotatedOffset = transform.rotation * _offset;

        // Ground detection box
        Gizmos.color = Color.blue;
        Vector3 groundBoxCenter = transform.position + rotatedOffset - transform.up * (_groundCastDistance / 2);
        Gizmos.DrawWireCube(groundBoxCenter, _boxSize);
    }
}
