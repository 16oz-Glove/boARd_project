using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeSetting : MonoBehaviour
{
    void Start()
    {
        /*
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        */

        Screen.orientation = ScreenOrientation.Landscape; //세로 가로방향을 나타냅니다.
        Screen.SetResolution(1920,1080, true);
    }
}
