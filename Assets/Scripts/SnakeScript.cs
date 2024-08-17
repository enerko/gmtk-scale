using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : MonoBehaviour
{
    public GameObject head;
    public float SnakeSpeed=10f;
    [SerializeField] private
    float headPosX;
    float headPosY;
    float distance;
    bool isReturning =false;
    // Start is called before the first frame update
    void Start()
    {
        headPosX=head.transform.position.x;
        headPosY=head.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReturning)
        {
            distance=Mathf.Sqrt(Mathf.Pow(head.transform.position.x-headPosX, 2f)+(Mathf.Pow(head.transform.position.y-headPosY, 2f)))*SnakeSpeed;
            if(distance<1f)
            {
                distance=1f;
                isReturning=false;
            }
            head.transform.position+=new Vector3((headPosX-head.transform.position.x)/distance,(headPosY-head.transform.position.y)/distance,0);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            isReturning=false;
            distance=Mathf.Sqrt(Mathf.Pow(head.transform.position.x-other.transform.position.x, 2f)+(Mathf.Pow(head.transform.position.y-other.transform.position.y, 2f)))*SnakeSpeed;
            if(distance<0.1f)
            {
                distance=0.1f;
            }
            head.transform.position+=new Vector3((other.transform.position.x-head.transform.position.x)/distance,(other.transform.position.y-head.transform.position.y)/distance,0);
        }

    }
    void OnTriggerExit2D(Collider2D other)
    {
        isReturning=true;
    }
}
