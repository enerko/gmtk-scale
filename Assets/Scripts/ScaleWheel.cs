using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWheel : MonoBehaviour
{
    [SerializeField] private
    int scaleAmount=-1;
    int currantColor=1;
    bool delay=false;
    // Start is called before the first frame update
    public void increaseScales()
    {
        scaleAmount+=1;
        if(scaleAmount>0 &&scaleAmount<=5)
        {
            transform.GetChild(scaleAmount-1).gameObject.SetActive(false);
        }
        if(scaleAmount<=6)
        {
            transform.GetChild(scaleAmount).gameObject.SetActive(true);
        }
    }
    void Start()
    {
        delay=false;
    }
    void Update()
    {
        //blue
        if(!delay)
        {
            delay=true;
        if(Input.GetKeyDown(KeyCode.Alpha1) &&scaleAmount>=0)
        {
            currantColor=1;
            StartCoroutine(changeBlue());
            transform.GetChild(7).gameObject.SetActive(true);
        }
        //red
        if(Input.GetKeyDown(KeyCode.Alpha2)&&scaleAmount>=1)
        {
            currantColor=2;
            StartCoroutine(changeRed());
            transform.GetChild(8).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)&&scaleAmount>=2)
        {
            currantColor=3;
            StartCoroutine(changeGreen());
            transform.GetChild(9).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)&&scaleAmount>=3)
        {
            currantColor=4;
            StartCoroutine(changeBrown());
            transform.GetChild(10).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)&&scaleAmount>=4)
        {
            currantColor=5;
            StartCoroutine(changePurple());
            transform.GetChild(11).gameObject.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)&&scaleAmount>=5)
        {
            currantColor=6;
            StartCoroutine(changeYellow());
            transform.GetChild(12).gameObject.SetActive(true);
        }
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            increaseScales();
        }
    }
    private void EmptyTail()
    {
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(false);
        transform.GetChild(11).gameObject.SetActive(false);
        transform.GetChild(12).gameObject.SetActive(false);
    }
    IEnumerator changeBlue()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeRed()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeGreen()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeBrown()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changePurple()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(11).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeYellow()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(12).gameObject.SetActive(true);
        delay=false;
    }
}
