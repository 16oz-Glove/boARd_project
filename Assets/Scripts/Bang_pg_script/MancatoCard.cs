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


    }

    protected override void Update_source()
    {
        
    }

    protected override void Update_animation()
    {
        stateName = "dodging";
    }

    //플레이어 4가 빗나감 카드를 사용함
    protected override void Update_Player()
    {
        UIManager.instance.Player4_to_Mantaco();
    }

}
