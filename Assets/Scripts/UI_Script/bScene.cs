using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class bScene : MonoBehaviour
{
    //"종료 하시겠습니까?" 하는 패널을 가리키는 오브젝트
    public GameObject exitButton;
    
    //뒤로가기 버튼을 눌렀을때, "게임종료하시겠습니까" 하는 패널 띄우기
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                if(exitButton.activeSelf)
                    exitButton.SetActive(false);
                else exitButton.SetActive(true);
            }
        }
    }


    //버튼 클릭시, SignIn Scene 로드
    public void OnclickButton_SceneLoad_SignIn()
    {
        SceneManager.LoadScene("SignIn");
    }

    public void OnclickButton_SceneLoad_ARrecognize_tt()
    {
        //버튼 누르면, AR인식하는 Scene으로 전환
        SceneManager.LoadScene("ARrecognize_tt");
    }

    public void OnclickButton_SceneLoad_ARrecognize_pg()
    {
        //버튼 누르면, AR인식하는 Scene으로 전환
        SceneManager.LoadScene("ARrecognize_pg");
    }

    public void OnclickButton_SceneLoad_Mainmenu()
    {
        SceneManager.LoadScene("Mainmenu");
    }

    public void OnclickButton_SceneLoad_Lobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnclickButton_SceneLoad_Bang_tt()
    {
        SceneManager.LoadScene("Bang_tt");
    }

    public void OnclickButton_SceneLoad_Bang_pg()
    {
        SceneManager.LoadScene("Bang_pg");
    }

    //Scene의 화면방향 정상적으로 전환
    public void Scene_scale()
    {
        Screen.orientation = ScreenOrientation.Portrait; //세로 방향을 나타냅니다.   
    }

    //어플 종료 함수
    public void Exit_Application()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

}
