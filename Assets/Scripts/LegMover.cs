using UnityEngine;

public class LegMover : MonoBehaviour
{
    [SerializeField] private Transform _limbSolverTarget;
    private float _moveDistance = 5f;
    [SerializeField] private LayerMask _groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();

        if (Vector2.Distance(_limbSolverTarget.position, transform.position) > _moveDistance)
        {
            _limbSolverTarget.position = transform.position;
        }
    }

    private void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 5, _groundLayer);
        if (hit.collider != null) 
        {
            Vector3 point = hit.point;
            point.y += 0.1f;
            transform.position = point;
        }
    }
}
