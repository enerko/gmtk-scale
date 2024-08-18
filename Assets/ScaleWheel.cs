using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWheel : MonoBehaviour
{
    [SerializeField] private
    int scaleAmount=0;
    // Start is called before the first frame update
    public void increaseScales()
    {
        scaleAmount+=1;
        if(scaleAmount>1)
        {
            transform.GetChild(scaleAmount-1).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        transform.GetChild(scaleAmount).gameObject.SetActive(true);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            increaseScales();
        }
    }
}
