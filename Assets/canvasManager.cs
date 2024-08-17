using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvasManager : MonoBehaviour
{
    public GameObject staminaBar, staminaParent;
    private float stamina;
    public int redPulseCount=3;
    public bool stamLost;
    private float time,t;
    public Image image;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //temporary stamina count by left shift, eventually Carmy tongue will trigger this
        if (Input.GetKey(KeyCode.LeftShift) &&!stamLost)
        {
            stamina-=1;
        }
        else
        {
            stamina+=0.5f;
        }
        if(stamina<-420)
        {
            stamLost=true;
        }
        if(stamina>0)
        {
            stamina=0;
        }
        staminaBar.GetComponent<RectTransform>().sizeDelta=new Vector2(stamina,0);
        if(stamLost)
        {
            time+=Time.deltaTime;
            t=-Mathf.Cos(5*time)/4+0.75f;

            Debug.Log(t);
            image.color = new Color(255, 0, 0, t);
            if(stamina>-1)
            {
                stamLost=false;
                image.color = new Color(255, 255, 255, t);
            }
            
        }
        
    }
}
