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
        animator3 = GameObject.Find("Bang/Ch_JOURDONNAIS/Rinnegato").GetComponent<Animator>();
        animator5 = GameObject.Find("Bang/Ch_ELGRINGO/Rinnegato").GetComponent<Animator>();
        animator7 = GameObject.Find("Bang/Ch_WILLYTHEKID/Rinnegato").GetComponent<Animator>();
        animator9 = GameObject.Find("Bang/Ch_SIDKETCHUM/Rinnegato").GetComponent<Animator>();
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

    }

}
