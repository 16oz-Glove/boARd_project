using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Card_event : MonoBehaviour
{
    bool gDraw = false;
    string boxContent;
    protected GameObject plane;
    protected GameObject model1;
    protected Animator animator1;
    protected Animator animator2;
    protected AudioSource source;
    protected string stateName;
    protected string stateName2;
    protected GameObject minimap;
    protected GameObject Panel_useCardModal;

    void Start()
    {
        Update_plane();
        Update_model();
        Update_animator();
        Update_animator2();
        Update_source();
        Update_animation();
        Update_animation2();
        Thread.Sleep(100);

        minimap = GameObject.Find("Canvas").transform.Find("Panel").transform.Find("Minimap").gameObject;   //미니맵 넣어주기
        Panel_useCardModal = GameObject.Find("Canvas").transform.Find("Panel_useCardModal").gameObject;     //카드 사용하시겠습니까? 패널
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            touchClick();
    }


    void touchClick()
    {
        RaycastHit hit;
        Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(touchray, out hit);

        if (hit.collider != null)
        {
            if (gDraw == false)
            {
                if (hit.collider.name == plane.name)
                {
                    plane.SetActive(false);
                    if (model1)
                    {
                        model1.SetActive(true);
                    }

                    if (animator1)
                        animator1.Play(stateName, -1, 0);
                    if (animator2)
                        animator2.Play(stateName2, -1, 0);

                    if (source) //오디오 소스가 존재하면 플레이 하고
                        source.Play();
                    Update_Player();    //플레이어 업데이트 --> 여기에 민지씨꺼 추가하면 될듯
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
    protected virtual void Update_plane()
    {

    }

    protected virtual void Update_model()
    {

    }

    protected virtual void Update_animator()
    {

    }

    protected virtual void Update_animator2()
    {

    }

    protected virtual void Update_source()
    {

    }

    protected virtual void Update_animation()
    {

    }

    protected virtual void Update_animation2()
    {

    }

    protected virtual void Update_Player()
    {

    }

}
