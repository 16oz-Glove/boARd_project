using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bang_card : MonoBehaviour
{
    public string obj;
    
    //bool gDraw = false;
    //public Vector2 touchpos;
    //string boxContent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcClick();
    }

    void ProcClick()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            ClickPosProc(Input.touches[i].position);
        }
    }

    //터치 좌표에 있는 오브젝트를 구한다.

    void ClickPosProc(Vector3 tPos)
    {

        Ray ray = Camera.main.ScreenPointToRay(tPos);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            obj = hit.collider.gameObject.name;

            //터치한 오브젝트 이름 출력
            print(obj);

            //// Solid_2라는 오브젝트 터치했을 때 애니메이션 수행
            //if (obj == "Solid_2")
            //{

            //    if (sol_2 == true)
            //    {
            //        IndyAnim.Play("open");
            //        sol_2 = false;
            //    }
            //    else
            //    {
            //        IndyAnim.Play("close");
            //        sol_2 = true;
            //    }
            //}
        }
    }


            //// 터치 시 오브젝트 확인 함수
            //void touchClick()
            //    {

            //        // 터치 입력이 들어올 경우
            //        if (Input.GetMouseButtonDown(0))
            //        {
            //            // 오브젝트 정보를 담을 변수 생성
            //            RaycastHit hit;

            //            // 터치 좌표를 담는 변수
            //            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //            // 터치한 곳에 ray를 보냄
            //            Physics.Raycast(touchray, out hit);

            //            // ray가 오브젝트에 부딪힐 경우
            //            if (hit.collider != null)
            //            {
            //                if (gDraw == false)
            //                {
            //                    gDraw = true;
            //                }
            //                else
            //                {
            //                    gDraw = false;
            //                }

            //                Debug.Log(hit.collider.gameObject.name);

            //            }

            //            if (Input.touchCount >= 1)
            //            {
            //                touchpos = Input.GetTouch(0).position;
            //                boxContent = "위치\r\n" + touchpos;
            //            }
            //        }

            //        void OnGUI()
            //        {
            //            if (gDraw == true)
            //            {
            //                GUI.Box(new Rect(10, 10, 100, 100), boxContent);
            //            }
            //        }
            //    }
        }
