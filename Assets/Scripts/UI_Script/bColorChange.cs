using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bColorChange : MonoBehaviour
{
    private Image img;
    public GameObject ob;
    private float red = 255/255f;
    private bool r_tf = true;
    private float green = 165 / 255f;
    private bool g_tf = false;
    private float blue = 165 / 255f;
    private bool b_tf = false;

    public int clear; //투명도

    void Start()
    {
        //등록된 오브젝트의 이미지 컴포넌트 받아오기.
        img = ob.GetComponent<Image>();
        
        ob.GetComponent<Image>().color = new Color(red, green, blue, clear/255f);  //기본 버튼 색 설정
    }

    void Update()
    {
        if(r_tf && !g_tf && !b_tf)    //그린 올라가는중  t,f,f
        {
            green += 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if(green == 255 / 255f || green > 255 / 255f)   //초록색이 255에 도착했거나 넘어가면
                g_tf = true;    //true로 변경.
        }
        else if(r_tf && g_tf && !b_tf)   //레드 내려가는중 t,t,f
        {
            red -= 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if (red == 165 / 255f || red < 165 / 255f)   //빨간색이 165에 도착했거나 더 작다면
                r_tf = false;    //false로 변경.
        }
        else if (!r_tf && g_tf && !b_tf)   //블루 올라가는중  f,t,f
        {
            blue += 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if (blue == 255 / 255f || blue > 255 / 255f)   //파란색이 255에 도착했거나 넘어가면
                b_tf = true;    //true로 변경.
        }
        else if (!r_tf && g_tf && b_tf)   //그린 내려가는중  f,t,t
        {
            green -= 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if (green == 165 / 255f || green < 165 / 255f)   //초록색이 165에 도착했거나 더 작다면
                g_tf = false;    //flase로 변경.
        }
        else if (!r_tf && !g_tf && b_tf)   //레드 올라가는중 f,f,t
        {
            red += 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if (red == 255 / 255f || red > 255 / 255f)   //빨간색이 255에 도착했거나 넘어가면
                r_tf = true;    //true로 변경.
        }
        else if (r_tf && !g_tf && b_tf)   //블루 내려가는중  t,f,t
        {
            blue -= 5 / 255f;
            img.color = new Color(red, green, blue, clear / 255f);  //버튼 색 설정
            if (blue == 165 / 255f || blue < 165 / 255f)   //파란색이 165에 도착했거나 더 작다면
                b_tf = false;    //false로 변경.
        }
        ob.GetComponent<Image>().color = img.color;
    }
}
