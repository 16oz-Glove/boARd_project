using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrigioneCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Prigione_card_Heart4/Plane_Prigione");
    }

    protected override void Update_model()
    {
        model1 = GameObject.Find("/Bang/Ch_VULTURESAM").transform.Find("jail").gameObject;

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
        stateName = "idle";
    }

    protected override void Update_Player()
    {
        model1.SetActive(false);    //자신한테는 감옥이 보이면 안되므로.
        UIManager.instance.Player1_To_Player4_Prision();    //감옥카드터치시, 누구에게 사용하라는 상태메시지

    }
}
