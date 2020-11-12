﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Bang_card : MonoBehaviour
{
    bool gDraw = false;
    string boxContent;
    public Animator animator;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();  //public변수이므로 굳이필요X
        Thread.Sleep(15000);
    }

    // Update is called once per frame
    void Update()
    {
        // 터치 입력이 들어올 경우
        if (Input.GetMouseButtonDown(0))
            touchClick();
    }


    // 터치 시 오브젝트 확인 함수
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
                animator.Play("CubeRotating", -1, 0);   //GUI박스 뜰때 애니메이션 실행
                gDraw = true;
                source.Play();
            }
            else
            {
                gDraw = false;
            }

            Debug.Log(hit.collider.gameObject.name);    //로그창에 오브젝트 이름 출력
 
        }

    }

    //void OnGUI()
    //{
    //    boxContent = "이름\r\n" + gameObject.name;    //오브젝트 이름

    //    if (gDraw == true)
    //    {
    //        GUI.Box(new Rect(10, 10, 100, 100), boxContent);
    //    }
    //}
    
}
