using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public string targetMsg;
    public int CPS; //Char Per Second
    Text msgText;
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
    }
    
    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();
    }


    void EffectStart()  //시작
    {
        msgText.text = "";
        index = 0;

        interval = 1.0f / CPS;
        Invoke("Effecting", interval);
    }

    void Effecting()    //재생
    {
        if (msgText.text == targetMsg)
        {
            return;
        }
        msgText.text += targetMsg[index];
        index++;

        Invoke("Effecting", interval);   //recursive
    }
}
