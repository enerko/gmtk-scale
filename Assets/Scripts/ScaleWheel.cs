using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWheel : MonoBehaviour
{
    public int currantColor=1;
    [SerializeField] private
    int scaleAmount=-1;
    bool delay=false;
    [SerializeField] private GameObject _animPlayer;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject[] _sprites;
    [SerializeField] private Color _blue;
    [SerializeField] private Color _green;
    [SerializeField] private Color _red;
    [SerializeField] private Color _yellow;
    [SerializeField] private Color _brown;
    [SerializeField] private Color _purple;
    [SerializeField] private AudioClip _clip;

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
        _animator = _animPlayer.GetComponent<Animator>();
        _player.SetActive(true);
        _animPlayer.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            increaseScales();
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

    private void StartChangingColor()
    {
        _player.SetActive(false);
        _animPlayer.SetActive(true);
        _animator.SetBool("IsChangingColor", true);
        SFXManager.PlayClip(_clip);
    }

    private void StopChangingColor()
    {
        _animator.SetBool("IsChangingColor", false);
        _animPlayer.SetActive(false);
        _player.SetActive(true);
        
    }
    private void ChangeColor(Color color)
    {
        // Color changing logic but it turns transparent rn, idk why
        for (int i = 0; i < _sprites.Length; i++)
        {
            SpriteRenderer spr = _sprites[i].GetComponent<SpriteRenderer>();
            spr.color = color;
        }
    }
    IEnumerator changeBlue()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        
        currantColor=1;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
        ChangeColor(_blue);
    }
    IEnumerator changeRed()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ChangeColor(_red);
        currantColor=2;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
    }
    IEnumerator changeGreen()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        ChangeColor(_red);
        currantColor =3;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
    }
    IEnumerator changeBrown()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=4;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
    }
    IEnumerator changePurple()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=5;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(11).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
    }
    IEnumerator changeYellow()
    {
        StartChangingColor();
        EmptyTail();
        transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        currantColor=6;
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(12).gameObject.SetActive(true);
        delay=false;
        StopChangingColor();
    }
}
