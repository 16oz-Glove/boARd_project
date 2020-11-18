using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Card_event : MonoBehaviour
{
    bool gDraw = false;
    string boxContent;
    public GameObject Plane;
    public GameObject Model;

    // Start is called before the first frame update
    void Start()
    {
        Thread.Sleep(100);
        Model.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            touchClick();
    }

    
    void touchClick()
    {

        // 오브젝트 정보를 담을 변수 생성
        RaycastHit hit;

        // 터치 좌표를 담는 변수
        Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 터치한 곳에 ray를 보냄
        Physics.Raycast(touchray, out hit);

        // ray가 오브젝트에 부딪힐 경우
        if (hit.collider != null)
        {
            if (gDraw == false)
            {
                if (hit.collider.name == Plane.name)
                {
                    Plane.SetActive(false);
                    Model.SetActive(true);
                }
                gDraw = true;
            }
            else
            {
                gDraw = false;
            }

            Debug.Log(hit.collider.gameObject.name);    //로그창에 오브젝트 이름 출력

        }

    }

    void OnGUI()
    {
        boxContent = "이름\r\n" + gameObject.name;    //오브젝트 이름

        if (gDraw == true)
        {
            GUI.Box(new Rect(10, 10, 100, 100), boxContent);
        }
    }
}
