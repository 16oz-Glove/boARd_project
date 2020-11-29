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
        model2 = GameObject.Find("/Bang/Ch_JOURDONNAIS").transform.Find("jail").gameObject;
        model3 = GameObject.Find("/Bang/Ch_ELGRINGO").transform.Find("jail").gameObject;
        model4 = GameObject.Find("/Bang/Ch_WILLYTHEKID").transform.Find("jail").gameObject;
        model5 = GameObject.Find("/Bang/Ch_SIDKETCHUM").transform.Find("jail").gameObject;
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
        stateName = "idle";
    }

    protected override void Update_Player()
    {

    }
}
