using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class LetsIN : MonoBehaviour
{
    public Vector2 touchpos;

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

        if (hit.collider != null && hit.transform.gameObject.tag == "BoardGame")
        {
            GameObject CurrentTouch = hit.transform.gameObject;
            CurrentTouch.GetComponent<BoardGame_info>().Touching_Board();
        }
    }

}
