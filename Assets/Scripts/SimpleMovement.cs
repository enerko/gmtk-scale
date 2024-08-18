using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontal;
    private float _vertical;
    private float _moveSpeed = 5f;

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
        _rb.velocity = new Vector2(_horizontal * _moveSpeed, _vertical * _moveSpeed);
    }

}
