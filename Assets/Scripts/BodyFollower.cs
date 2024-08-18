using UnityEngine;

public class BodyFollower : MonoBehaviour
{
    public Transform head;
    public Transform[] bodySegments;
    public float smoothTime = 0.1f;
    public float _moveDistance = 5.0f;

    private Vector3[] _velocity;

    void Start()
    {
        _velocity = new Vector3[bodySegments.Length];
    }

    void Update()
    {
        if (Vector2.Distance(head.position, transform.position) > _moveDistance)
        {
            // Calculate the direction from the head to the current position
            Vector2 direction = (transform.position - head.position).normalized;

            // Move the head position closer to the current position by a fraction
            head.position += (Vector3)direction * (_moveDistance / 2);
        }
    }
}