using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelloCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Duello_card_SpaJ/Plane_duello");
    }

    protected override void Update_model()
    {
        model1 = GameObject.Find("/Bang/Ch_VULTURESAM").transform.Find("duel_letter").gameObject;

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
        stateName = "Duel";
    }
    /*
    //if 결투에서 승리하면
    protected override void Update_animation()
    {
        stateName = "DuelWinner";
    }
    */
    protected override void Update_Player()
    {
        UIManager.instance.Duo_event();
    }

}
