using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBalouCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/CatBalou_card_DiaJ/Plane_cat");
    }

    protected override void Update_model()
    {

    }

    protected override void Update_animator()
    {
        animator1 = GameObject.Find("Bang/CatBalou_card_DiaJ/CatBalou@Idle").GetComponent<Animator>();

    }

    protected override void Update_source()
    {
        
    }

    protected override void Update_animation()
    {
        stateName = "pointing";
    }

    protected override void Update_Player()
    {
        Debug.Log("캣벌로우 카드 효과 발동");
        UIManager.instance.Player_Action__State_Please("바로 오른쪽 사람에게 \"캣 벌루오\"카드를 사용해 보겠습니다.\n 미니맵에서 오른쪽 사용자의 오브젝트를 터치해 주세요.");
    }

}
