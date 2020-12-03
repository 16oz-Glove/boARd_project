using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanicoCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Panico_card_HeartA/Plane_Panico");
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
        
    }

    protected override void Update_animation()
    {
        stateName = "pointing";
    }

    protected override void Update_Player()
    {
        Debug.Log("강탈 카드 효과 발동");
        UIManager.instance.Player1_Turn2_Text();
    }

}
