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
  
}
