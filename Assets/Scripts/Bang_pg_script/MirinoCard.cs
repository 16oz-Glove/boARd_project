using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirinoCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Mirino_card_SpaA/Plane_mirino");
    }

    protected override void Update_model()
    {
        model1 = GameObject.Find("/Bang/Ch_VULTURESAM").transform.Find("scope").gameObject;

    }

    protected override void Update_animator()
    {
        animator1 = GameObject.Find("Bang/Ch_VULTURESAM/Rinnegato").GetComponent<Animator>();

    }

    protected override void Update_source()
    {
        source = GameObject.Find("/Bang/Mirino_card_SpaA/audio_mirino").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "Rifle_down_to_aim";
    }

    protected override void Update_Player()
    {
        UIManager.instance.mirino.SetActive(true);//조준경 이미지 활성화
        UIManager.instance.Player_Button[1].GetComponent<PlayerSet>().Minus_Far_All();  //거리 1씩 감소
        UIManager.instance.Player_Button[1].GetComponent<PlayerSet>().UpdateUI_Far();   //거리 1식 감소한거 UI로 띄워주기
        UIManager.instance.Player2_All_state("님께서 \"조준경\" 카드를 사용하셨습니다.");   //다른 사용자들에게 보여질 State문구
        UIManager.instance.Player_Action__State_Please("각 플레이어들로 부터 내 거리가 1씩 가까워졌습니다.");   //나에게 보여질 state문구
        UIManager.instance.mirino_Panel.SetActive(true);        //조준경 패널 활성화
    }


}
