using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RirraCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Rirra_card_Heart10/Plane_Rirra");
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
        source = GameObject.Find("/Bang/Rirra_card_Heart10/Audio_rirra").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "drinking";
    }

    protected override void Update_Player()
    {
        UIManager.instance.Rirra_Panel.SetActive(true);
        UIManager.instance.Player_Button[4].GetComponent<PlayerSet>().Plus_life(1); //생명력 +1 해주고 화면에도 라이프 수정해주기
    }

}
