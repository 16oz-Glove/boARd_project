using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Bang_card_Dia7/Plane_bang");
    }

    protected override void Update_model()
    {

    }

    protected override void Update_animator()
    {
        animator1 = GameObject.Find("Bang/Ch_VULTURESAM/Rinnegato").GetComponent<Animator>();


    }

    protected override void Update_animator2()
    {
        animator2 = GameObject.Find("Bang/Ch_VULTURESAM/ShootPistolwithpistol").GetComponent<Animator>();


    }

    protected override void Update_source()
    {
        source = GameObject.Find("/Bang/Bang_card_Dia7/Audio_bang").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "bang";
    }

    protected override void Update_animation2()
    {
        stateName2 = "pistol_move";
    }
    /*
    protected override void Update_animation2()
    {
        stateName2 = "action";
    }
    */
    protected override void Update_Player()
    {
        Debug.Log("뱅 카드 효과 발동");
        if (UIManager.instance.Player_Turn == 1)    //플레이어 1일때
            UIManager.instance.Player_Action__State_Please("바로 오른쪽 사람에게 \"뱅\"카드를 사용해 보겠습니다.\n 미니맵에서 오른쪽 사용자의 오브젝트를 터치해 주세요.");
        else if (UIManager.instance.Player_Turn == 3) UIManager.instance.Player3_To_Player4_Bang_Event();   //플레이어 3일때는 4에게 공격한다.
        else UIManager.instance.Player2_To_Player3_Bang();  //플레이어2 차례일때, 미니맵을 열어 터치해 달라는 상태메시지 업뎃
    }
}
