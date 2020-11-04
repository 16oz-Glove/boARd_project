using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bSound : MonoBehaviour
{
    new AudioSource audio;
    //private bool isbutton = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    //버튼을 누르면, 
    public void onbutton()
    {
        //소리가 나오는 중이라면,
        if (audio.mute)
        {
            //소리 끄기.
            audio.mute = false;
        }
        else //소리가 나오는 중이 아니라면
        {
            //소리 켜기
            audio.mute = true;
        }
    }

}
