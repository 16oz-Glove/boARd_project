using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndianiCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Indiani_card_DiaA/Plane_Indian");
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
        stateName = "YellOut";
    }

    protected override void Update_Player()
    {
        UIManager.instance.Player3_To_Indian_all(); //다른 사용자들에게 indiancard 패널 띄우기
    }

}