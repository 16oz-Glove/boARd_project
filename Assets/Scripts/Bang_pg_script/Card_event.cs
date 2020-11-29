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
    protected GameObject model2;
    protected GameObject model3;
    protected GameObject model4;
    protected GameObject model5;
    protected Animator animator1;
    protected Animator animator2;
    protected Animator animator3;
    protected Animator animator4;
    protected Animator animator5;
    protected Animator animator6;
    protected Animator animator7;
    protected Animator animator8;
    protected Animator animator9;
    protected Animator animator10;
    protected AudioSource source;
    protected string stateName;
    protected string stateName2;

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
                        model2.SetActive(true);
                        model3.SetActive(true);
                        model4.SetActive(true);
                        model5.SetActive(true);
                    }

                    if (animator1)
                        animator1.Play(stateName, -1, 0);
                    if (animator2)
                        animator2.Play(stateName2, -1, 0);

                    if (animator3)
                        animator3.Play(stateName, -1, 0);
                    if (animator4)
                        animator4.Play(stateName2, -1, 0);

                    if (animator5)
                        animator5.Play(stateName, -1, 0);
                    if (animator6)
                        animator6.Play(stateName2, -1, 0);

                    if (animator7)
                        animator7.Play(stateName, -1, 0);
                    if (animator8)
                        animator8.Play(stateName2, -1, 0);

                    if (animator9)
                        animator9.Play(stateName, -1, 0);
                    if (animator10)
                        animator10.Play(stateName2, -1, 0);

                    if (source)
                        source.Play();
                    Update_Player();
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
    /*
    void OnGUI()
    {
        boxContent = "이름\r\n" + gameObject.name;    //오브젝트 이름

        if (gDraw == true)
        {
            GUI.Box(new Rect(10, 10, 100, 100), boxContent);
        }
    }
    */
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
