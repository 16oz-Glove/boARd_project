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
        model2 = GameObject.Find("/Bang/Ch_JOURDONNAIS").transform.Find("duel_letter").gameObject;
        model3 = GameObject.Find("/Bang/Ch_ELGRINGO").transform.Find("duel_letter").gameObject;
        model4 = GameObject.Find("/Bang/Ch_WILLYTHEKID").transform.Find("duel_letter").gameObject;
        model5 = GameObject.Find("/Bang/Ch_SIDKETCHUM").transform.Find("duel_letter").gameObject;
    }

    protected override void Update_animator()
    {
        animator1 = GameObject.Find("Bang/Ch_VULTURESAM/Rinnegato").GetComponent<Animator>();
        animator3 = GameObject.Find("Bang/Ch_JOURDONNAIS/Rinnegato").GetComponent<Animator>();
        animator5 = GameObject.Find("Bang/Ch_ELGRINGO/Rinnegato").GetComponent<Animator>();
        animator7 = GameObject.Find("Bang/Ch_WILLYTHEKID/Rinnegato").GetComponent<Animator>();
        animator9 = GameObject.Find("Bang/Ch_SIDKETCHUM/Rinnegato").GetComponent<Animator>();

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

    }

}
