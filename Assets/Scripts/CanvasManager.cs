using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject staminaBar, staminaParent;
    public Image image, image2;
    public float fadeDelay=2f;
    [SerializeField] private
    float stamVal;
    bool isStamLost;
    bool isActivated=false;
    float time,t,fades=0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fades>1)
        {
            fades=1;
        }
        if(fades<0)
        {
            fades=0;
        }
        if(!isStamLost)
        {
            if(isActivated)
            {
                fades+=Time.deltaTime;
                image.color = new Color(255, 255, 255, fades);
                image2.color = new Color(255, 255, 255, fades/4);
            }
            else
            {
                fades-=Time.deltaTime;
                image.color = new Color(255, 255, 255, fades);
                image2.color = new Color(255, 255, 255, fades/4);
            }
        }

        //temporary stamina count by left shift, eventually Carmy tongue will trigger this
        if (Input.GetKey(KeyCode.LeftShift) &&!isStamLost)
        {
            stamVal-=1;
            isActivated=true;
        }
        else
        {
            stamVal+=0.5f;
        }
        if(stamVal<-470)
        {
            isStamLost=true;
        }
        if(stamVal>0)
        {
            stamVal=0;
            isActivated=false;
        }
        staminaBar.GetComponent<RectTransform>().sizeDelta=new Vector2(stamVal,0);
        if(isStamLost)
        {
            time+=Time.deltaTime;
            t=-Mathf.Cos(5*time)/4+0.75f;
            image.color = new Color(255, 0, 0, t);
            if(stamVal>-1)
            {
                isStamLost=false;
                image.color = new Color(255, 255, 255, 1f);
            }
            
        }
        
    }
}
