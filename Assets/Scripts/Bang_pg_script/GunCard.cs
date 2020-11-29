using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCard : Card_event
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

    protected override void Update_animator2()
    {
        animator2 = GameObject.Find("Bang/Ch_VULTURESAM/ShootPistolwithpistol").GetComponent<Animator>();
        animator4 = GameObject.Find("Bang/Ch_JOURDONNAIS/ShootPistolwithpistol").GetComponent<Animator>();
        animator6 = GameObject.Find("Bang/Ch_ELGRINGO/ShootPistolwithpistol").GetComponent<Animator>();

        animator8 = GameObject.Find("Bang/Ch_WILLYTHEKID/ShootPistolwithpistol").GetComponent<Animator>();

        animator10 = GameObject.Find("Bang/Ch_SIDKETCHUM/ShootPistolwithpistol").GetComponent<Animator>();

    }

    protected override void Update_source()
    {
        source = GameObject.Find("/Bang/Bang_card_Dia7/Audio_bang").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "bang";
    }

    protected override void Update_animation2()
    {
        stateName2 = "pistol_move";
    }
    /*
    protected override void Update_animation2()
    {
        stateName2 = "action";
    }
    */
    protected override void Update_Player()
    {

    }
}
