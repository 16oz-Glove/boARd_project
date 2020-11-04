using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bImageChange : MonoBehaviour
{
    public Sprite changed_Img1;  //버튼 눌러서 바뀔 스프라이트 이미지1 넣을 부분
    public Sprite changed_Img2;  //버튼 눌러서 바뀔 스프라이트 이미지2 넣을 부분
    public GameObject selfgb;   //자기 자신의 오브젝트

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
    }

    // 버튼이 눌렸을때, 이미지 바뀌게 하기
    public void Onclick_imgChange()
    {

        if (button.image.overrideSprite == changed_Img1)
        {
            button.image.overrideSprite = changed_Img2;
        }
        else
        {
            button.image.overrideSprite = changed_Img1;
        }
    }
}
