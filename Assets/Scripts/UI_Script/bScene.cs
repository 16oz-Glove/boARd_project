using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class bScene : MonoBehaviour
{
    //버튼 클릭시, SignIn Scene 로드
    public void OnclickButton_SceneLoad1()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void OnclickButton_SceneLoad2()
    {
        //버튼 누르면, AR인식하는 Scene으로 전환
        SceneManager.LoadScene("ARrecognize");
    }

    //Scene의 화면방향 정상적으로 전환
    public void Scene_scale()
    {
        Screen.orientation = ScreenOrientation.Portrait; //세로 방향을 나타냅니다.   
    }

}
