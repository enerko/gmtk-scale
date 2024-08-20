using UnityEngine;

public class BodyFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {

    }

    void Update()
    {
        transform.position = target.position + offset;
        transform.rotation = target.rotation;
        transform.localScale = target.localScale;
    }
}