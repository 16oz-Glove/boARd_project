using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirinoCard : Card_event
{
    protected override void Update_plane()
    {
        plane = GameObject.Find("/Bang/Mirino_card_SpaA/Plane_mirino");
    }

    protected override void Update_model()
    {
        model1 = GameObject.Find("/Bang/Ch_VULTURESAM").transform.Find("scope").gameObject;
        model2 = GameObject.Find("/Bang/Ch_JOURDONNAIS").transform.Find("scope").gameObject;
        model3 = GameObject.Find("/Bang/Ch_ELGRINGO").transform.Find("scope").gameObject;
        model4 = GameObject.Find("/Bang/Ch_WILLYTHEKID").transform.Find("scope").gameObject;
        model5 = GameObject.Find("/Bang/Ch_SIDKETCHUM").transform.Find("scope").gameObject;
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
        source = GameObject.Find("/Bang/Mirino_card_SpaA/audio_mirino").GetComponent<AudioSource>();
    }

    protected override void Update_animation()
    {
        stateName = "Rifle_down_to_aim";
    }

    protected override void Update_Player()
    {
        
    }


}
