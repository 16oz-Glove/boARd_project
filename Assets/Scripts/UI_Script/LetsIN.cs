using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class LetsIN : MonoBehaviour
{
    //bool gDraw = false;
    public Vector2 touchpos;
    //string boxContent;

    // Update is called once per frame
    void Update()
    {
        // 터치 입력이 들어올 경우
        if (Input.GetMouseButtonDown(0))
            touchClick();

    }

    //터치한 오브젝트의 이름을 가져와서, 해당하는 Scene 가져온다.
    void touchClick()
    {
        // 오브젝트 정보를 담을 변수 생성
        RaycastHit hit;

        // 터치 좌표를 담는 변수
        Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 터치한 곳에 ray를 보냄
        Physics.Raycast(touchray, out hit);

        if (hit.collider != null)
        {
            GameObject CurrentTouch = hit.transform.gameObject;
            FindObjectWithName(CurrentTouch);
        }

    }

    // 입력받은오브젝트이름으로찾기
    void FindObjectWithName(GameObject CurrentTouch)
    {
       GameObject tempObj = null;      //임시오브젝트생성
       tempObj = CurrentTouch;
       if (tempObj != null)             //오브젝트를성공적으로받았다면
       {
            Debug.Log("성공적으로" + tempObj.name + "오브젝트를받았습니다");
            if (tempObj.name == "Bang.pg")
            {
                string boardname = tempObj.name;
                //버튼 누르면,뱅 연습게임 Scene로 이동
                SceneManager.LoadScene(boardname);
            }
            else
            {
                Debug.Log("성공적으로 오브젝트를 받지 못했습니다");
            }
       }
    }


}
