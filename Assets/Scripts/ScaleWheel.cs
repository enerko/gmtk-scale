using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWheel : MonoBehaviour
{
    public int currantColor=5;
    [SerializeField] private
    int scaleAmount=-1;
    bool delay=false;

    private Animator _animator;
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
        if(scaleAmount==0)
        {
            transform.GetChild(7).gameObject.SetActive(true);
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
        if(Input.GetKeyDown(KeyCode.Alpha1) &&scaleAmount>=0)
        {
            delay=true;
            StartCoroutine(changeBlue());
        }
        //red
        if(Input.GetKeyDown(KeyCode.Alpha2)&&scaleAmount>=1)
        {
            delay=true;
            StartCoroutine(changeRed());
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)&&scaleAmount>=2)
        {
            delay=true;
            StartCoroutine(changeGreen());
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)&&scaleAmount>=3)
        {
            delay=true;
            StartCoroutine(changeBrown());
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)&&scaleAmount>=4)
        {
            delay=true;
            StartCoroutine(changePurple());
        }
        if(Input.GetKeyDown(KeyCode.Alpha6)&&scaleAmount>=5)
        {
            delay=true;
            StartCoroutine(changeYellow());
        }
        }
    }
    private void EmptyTail()
    {
        currantColor=0;
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
        
        currantColor=1;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeRed()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=2;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeGreen()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor =3;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeBrown()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=4;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changePurple()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=5;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(11).gameObject.SetActive(true);
        delay=false;
    }
    IEnumerator changeYellow()
    {
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=6;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(12).gameObject.SetActive(true);
        delay=false;
    }
}
