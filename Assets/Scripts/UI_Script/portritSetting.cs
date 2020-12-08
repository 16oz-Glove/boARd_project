using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portritSetting : MonoBehaviour
{
    void Start()
    {
        /*
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        */
        Screen.orientation = ScreenOrientation.Portrait; //세로방향을 나타냅니다.   
        Screen.SetResolution(1080, 1920, true);
    }
}
