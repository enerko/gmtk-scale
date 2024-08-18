using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontal;
    private float _vertical;
    private float tonguePosX,tonguePosY, distance;
    private bool isSwinging=false, isExtending=false, isFound=false;
    private Vector3 point = new Vector3(), pointIn= new Vector3();
    private Vector3[] points = new Vector3[2];

    public float _moveSpeed = 5f;
    public Vector2 boxSize = new Vector2(1f, 2f);
    public float groundCastDistance = 0.1f;
    public float wallCastDistance = 0.1f;
    public LayerMask groundLayer;
    public float _rotationSpeed = 10f;
    public Camera MainCam;
    public GameObject tongue;

    private Vector3 _direction; // 1 if facing right, -1 if facing left

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.GetComponent<DistanceJoint2D>().enabled=false;
    }

    void Update()
    {
        // Get horizontal and vertical input
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (IsNextToAWall() && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            RotateToAlignWithWall();
        }

        if (!IsNextToAWall())
        {
            HandleMovement();
        }
        if(!IsGrounded())
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
        else
        {
            Physics2D.gravity = new Vector2(0, 0);
        }
        if(isExtending)
        {
            tongue.SetActive(true);
            pointIn=MainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, MainCam.nearClipPlane));
            distance = Mathf.Sqrt(Mathf.Pow(pointIn[0]-transform.position.x,2)+Mathf.Pow(pointIn[1]-transform.position.y,2));
            tongue.transform.position+= new Vector3((pointIn[0]-transform.position.x)/distance/5,(pointIn[1]-transform.position.y)/distance/5,0);
            points[0] = new Vector3(transform.position.x, transform.position.y, -0.7f);
            points[1] = new Vector3(tongue.transform.position.x, tongue.transform.position.y, -0.7f);
            tongue.transform.GetComponent<LineRenderer>().SetPositions(points);
            if(isFound)
            {
                tonguePosX=tongue.transform.position.x;
                tonguePosY=tongue.transform.position.y;
                isSwinging=true;
                isExtending=false;
            }
        }
        if(Input.GetMouseButton(1))
        {
            if(!isSwinging)
            {
                isExtending=true;
            }
            else
            {
                transform.GetComponent<DistanceJoint2D>().enabled=true;
                transform.GetComponent<DistanceJoint2D>().connectedAnchor=new Vector2(tonguePosX,tonguePosY);
                transform.GetComponent<DistanceJoint2D>().distance+=Input.GetAxis("Mouse ScrollWheel")*4;

                

                points[0] = new Vector3(transform.position.x, transform.position.y, -0.7f);
                points[1] = new Vector3(tongue.transform.position.x, tongue.transform.position.y, -0.7f);
                tongue.transform.GetComponent<LineRenderer>().SetPositions(points);
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            isSwinging=false;
            isExtending=false;
            transform.GetComponent<DistanceJoint2D>().enabled=false;
            tongue.SetActive(false);
            isFound=false;
            tongue.transform.position= new Vector2(transform.position.x,transform.position.y);
        }
    }
    public void foundPath()
    {
        isFound=true;
    }
    private bool IsGrounded()
    {
        // Check if the player is grounded
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCastDistance, groundLayer);
        return hit.collider != null;
    }

    private bool IsNextToAWall()
    {
        // Check if the player is next to and facing a wall
        Vector2 boxCastOrigin = transform.position + _direction * (boxSize.y / 2);
        RaycastHit2D hit = Physics2D.BoxCast(boxCastOrigin, boxSize, 0, _direction, wallCastDistance, groundLayer);
        return hit.collider != null;
    }

    private void RotateToAlignWithWall()
    {
        // Rotate the player
        float angle = CalculateRotateAngle();

        // Rotate the player
        transform.Rotate(0, 0, angle);

        // Rotate the _direction vector by the calculated angle
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Apply the rotation to the _direction vector
        _direction = rotation * _direction;
    }

    private void HandleMovement()
    {
        // If grounded, handle vertical movement as well
        if (IsGrounded())
        {
            if (IsPlayerMovingVertical())
            {
                // Is player movement vertical
                // If moving, check direction 
                if (_vertical != 0) _direction.y = (int)(Mathf.Sign(_vertical));
                _rb.velocity = new Vector2(0, _vertical * _moveSpeed);
            }
            else
            {
                // If moving, check direction 
                if (_horizontal != 0) { _direction.x = (int)Mathf.Sign(_horizontal); }

                _rb.velocity = new Vector2(_horizontal * _moveSpeed, 0);
            }
        }
    }

    private bool IsPlayerMovingVertical()
    {
        // Check that the vertical (W and D) movement is perpendicular to the wall
        Vector2 verticalDirection = Vector2.up;
        Vector2 playerDownDirection = -transform.up;
        float dotProduct = Vector2.Dot(verticalDirection, playerDownDirection);

        return Mathf.Abs(dotProduct) < 0.01f; // Using a small threshold for floating point precision
    }

    private float CalculateRotateAngle()
    {
        // Get the player's down direction and right direction
        Vector2 downDir = -transform.up;

        // Calculate the angle between downDirection and rightDirection
        float angle = Vector2.Angle(downDir, _direction);

        // clockwise or counterclockwise
        float crossProduct = downDir.x * _direction.y - downDir.y * _direction.x;
        if (crossProduct < 0)
        {
            angle = -angle;
        }

        return angle;
    }

    private void OnDrawGizmos()
    {
        // Wall detection box
        Gizmos.color = Color.red;
        Vector3 boxCenter = transform.position + _direction * (wallCastDistance / 2);
        Gizmos.DrawWireCube(boxCenter, boxSize);

        // Ground detection box
        Gizmos.color = Color.blue;
        Vector3 groundBoxCenter = transform.position - transform.up * (groundCastDistance / 2);
        Gizmos.DrawWireCube(groundBoxCenter, boxSize);
    }
}
