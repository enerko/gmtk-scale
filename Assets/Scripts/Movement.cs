using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _horizontal = 0;
    private float _moveSpeed = 5f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");

    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(_horizontal * _moveSpeed, _rb.velocity.y);
    }
}
