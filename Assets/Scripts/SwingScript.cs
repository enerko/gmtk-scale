using UnityEngine;

public class SwingScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private float tonguePosX,tonguePosY, distance, distance2;
    private bool isSwinging=false, isExtending=false, isFound=false;
    private Vector3 point = new Vector3(), pointIn= new Vector3();
    private Vector3[] points = new Vector3[2];
    private bool isOverheated=false, isRetracting=false, isMissed=false;

    public Vector2 boxSize = new Vector2(1f, 2f);
    public float groundCastDistance = 0.1f;
    public LayerMask groundLayer;
    public Camera MainCam;
    public GameObject tongue;
    public Canvas canvas;
    public GameObject path;

    private Vector3 _direction; // 1 if facing right, -1 if facing left

    void Start()
    {
        tongue.transform.position= new Vector2(transform.position.x,transform.position.y);
        tongue.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        transform.GetComponent<DistanceJoint2D>().enabled=false;
    }

    void Update()
    {
        if(!IsGrounded())
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
        }
        else
        {
            path.layer=7;
            this.GetComponent<SimpleMovement>().enabled=true;
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
                if(Mathf.Sqrt(Mathf.Pow(tongue.transform.position.x-transform.position.x,2)+Mathf.Pow(tongue.transform.position.y-transform.position.y,2))>15)
                {
                    isMissed=true;
                    retract();
                }
                else
                {
                    tonguePosX=tongue.transform.position.x;
                    tonguePosY=tongue.transform.position.y;
                    isSwinging=true;
                    isExtending=false;
                }
                
            }
        }
        if(Input.GetMouseButton(1) && !isOverheated &&!isMissed)
        {
            isRetracting=false;
            if(!isSwinging)
            {
                isExtending=true;
            }
            else
            {
                path.layer=8;
                this.GetComponent<SimpleMovement>().enabled=false;
                canvas.GetComponent<CanvasManager>().StaminaDeplete();
                transform.GetComponent<DistanceJoint2D>().enabled=true;
                transform.GetComponent<DistanceJoint2D>().connectedAnchor=new Vector2(tonguePosX,tonguePosY);
                if(Input.GetAxis("Mouse ScrollWheel")<0 && !IsRoofed())
                {
                    transform.GetComponent<DistanceJoint2D>().distance+=Input.GetAxis("Mouse ScrollWheel")*4;
                    canvas.GetComponent<CanvasManager>().StaminaDeplete();
                }
                if(Input.GetAxis("Mouse ScrollWheel")>0 &&!IsGrounded())
                {
                    transform.GetComponent<DistanceJoint2D>().distance+=Input.GetAxis("Mouse ScrollWheel")*4;
                    canvas.GetComponent<CanvasManager>().StaminaIncrease();
                }

                

                points[0] = new Vector3(transform.position.x, transform.position.y, -0.7f);
                points[1] = new Vector3(tongue.transform.position.x, tongue.transform.position.y, -0.7f);
                tongue.transform.GetComponent<LineRenderer>().SetPositions(points);
            }
        }
        if(Input.GetMouseButtonUp(1))
        {
            retract();
        }
        if(isOverheated)
        {
            isSwinging=false;
            isExtending=false;
            transform.GetComponent<DistanceJoint2D>().enabled=false;
            tongue.SetActive(false);
            isFound=false;
            tongue.transform.position= new Vector2(transform.position.x,transform.position.y);
        }
        if(isRetracting)
        {
            points[0] = new Vector3(transform.position.x, transform.position.y, -0.7f);
            points[1] = new Vector3(tongue.transform.position.x, tongue.transform.position.y, -0.7f);
            tongue.transform.GetComponent<LineRenderer>().SetPositions(points);
            distance2 = Mathf.Sqrt(Mathf.Pow(transform.position.x-tongue.transform.position.x,2)+Mathf.Pow(transform.position.y-tongue.transform.position.y,2));
            tongue.transform.position+= new Vector3((transform.position.x-tongue.transform.position.x)/distance,(transform.position.y-tongue.transform.position.y)/distance,0);
            if(Mathf.Abs(transform.position.x-tongue.transform.position.x)<2f&&Mathf.Abs(transform.position.y-tongue.transform.position.y)<0.1f)
            {
                isMissed=false;
                isRetracting=false;
                tongue.SetActive(false);
            }
        }
    }
    public void foundPath()
    {
        isFound=true;
    }
    public void OverHeat()
    {
        isOverheated=true;
    }
    public void CoolDown()
    {
        isOverheated=false;
    }
    private bool IsGrounded()
    {
        // Check if the player is grounded
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCastDistance, groundLayer);
        return hit.collider != null;
    }
    private bool IsRoofed()
    {
        // Check if the player is grounded
        RaycastHit2D hit1 = Physics2D.BoxCast(transform.position, boxSize, 0, transform.up, groundCastDistance, groundLayer);
        return hit1.collider != null;
    }
    private void retract()
    {
        isRetracting=true;
        isSwinging=false;
        isExtending=false;
        transform.GetComponent<DistanceJoint2D>().enabled=false;
        isFound=false;
    }
}
