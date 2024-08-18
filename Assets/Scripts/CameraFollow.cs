using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float _followSpeed = 4f;
    [SerializeField] private Transform _target;
    private float _yOffset = 1f;

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(_target.position.x, _target.position.y + _yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
    }
}
