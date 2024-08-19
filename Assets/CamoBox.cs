using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamoBox : MonoBehaviour
{
    public GameObject ScaleWheel;
    public int color=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            if(ScaleWheel.GetComponent<ScaleWheel>().currantColor==color)
            {
                Debug.Log("CAMO");
            }
            else
            {
                Debug.Log("NO CAMO");
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            Debug.Log("NO CAMO");
        }
    }
}
