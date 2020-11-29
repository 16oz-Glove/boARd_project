using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinchesterCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Winchester_card_Spa8/Plane_winchester");
    }

    protected override void Update_model()
    {
        model1 = GameObject.Find("/Bang/Ch_VULTURESAM").transform.Find("Rifle").gameObject;
        model2 = GameObject.Find("/Bang/Ch_JOURDONNAIS").transform.Find("Rifle").gameObject;
        model3 = GameObject.Find("/Bang/Ch_ELGRINGO").transform.Find("Rifle").gameObject;
        model4 = GameObject.Find("/Bang/Ch_WILLYTHEKID").transform.Find("Rifle").gameObject;
        model5 = GameObject.Find("/Bang/Ch_SIDKETCHUM").transform.Find("Rifle").gameObject;


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
        stateName = "Rifle_idle";
    }

    protected override void Update_Player()
    {

    }

}
