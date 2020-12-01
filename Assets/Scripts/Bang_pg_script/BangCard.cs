using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangCard : Card_event
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

    protected override void Update_source()
    {
        source = GameObject.Find("/Bang/Bang_card_Dia7/Audio_bang").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "bang";
    }
    /*
    protected override void Update_animator2()//pistol
    {
        animator = GameObject.Find("Bang/Ch_VULTURESAM/pistol").GetComponent<Animator>();
    }

    protected override void Update_animation()//pistol
    {
        stateName = "pistol_move";
    }
    */
    protected override void Update_Player()
    {
        Debug.Log("뱅 카드 효과 발동");
        UIManager.instance.Player_Action__State_Please("바로 오른쪽 사람에게 \"뱅\"카드를 사용해 보겠습니다.\n 미니맵에서 오른쪽 사용자의 오브젝트를 터치해 주세요.");
    }

}
