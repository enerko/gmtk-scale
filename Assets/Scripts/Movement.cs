using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float _horizontal;
    private float _vertical;
    private float gravVal, disVal1, disVal2, mousePosX,mousePosY;
    private bool isSwinging=false;
    private Vector3 point = new Vector3();

    public float _moveSpeed = 5f;
    public Vector2 boxSize = new Vector2(1f, 2f);
    public float groundCastDistance = 0.1f;
    public float wallCastDistance = 0.1f;
    public LayerMask groundLayer;
    public float _rotationSpeed = 10f;
    public Camera MainCam;

    private Vector3 _direction; // 1 if facing right, -1 if facing left

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            gravVal-=0.1f;
            transform.position+=new Vector3(0,-16*Mathf.Pow(gravVal*Time.deltaTime,2),0);
        }
        else
        {
            gravVal=0;
        }
        if(Input.GetMouseButton(0))
        {
            // Swing((MousePos.x,MousePos.y));
            if(!isSwinging)
            {
                point=MainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.x, MainCam.nearClipPlane));
                Debug.Log(point);
                mousePosX=point[0];
                mousePosY=point[1];
                disVal1=Mathf.Sqrt(Mathf.Pow(mousePosX-transform.position[0],2f)+Mathf.Pow(mousePosY-transform.position[1],2f));
                isSwinging=true;
            }
            else
            {
                disVal2=Mathf.Sqrt(Mathf.Pow(mousePosX-transform.position[0],2f)+Mathf.Pow(mousePosY-transform.position[1],2f));
                if(disVal2>disVal1)
                {
                    if(gravVal>10f)
                    {
                        gravVal=10f;
                    }
                    Debug.Log("OUT OF RANGE");
                    _rb.velocity= new Vector2(((disVal2-disVal1)/disVal2)*(mousePosX-transform.position.x)*300*Time.deltaTime,((disVal2-disVal1)/disVal2)*(mousePosY-transform.position.y)*300*Time.deltaTime);
                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isSwinging=false;
        }
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
