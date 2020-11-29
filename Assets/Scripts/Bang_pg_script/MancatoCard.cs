using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MancatoCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Mancato_cad_Spa5/Plane_mancato");
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
        
    }

    protected override void Update_animation()
    {
        stateName = "dodging";
    }

    protected override void Update_Player()
    {

    }

}
