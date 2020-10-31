using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class Sceneorientation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft; //가로 방향을 나타내며, 세로 방향으로부터 반 시계방향으로 회전한 상태를 나타냅니다.
    }

}
