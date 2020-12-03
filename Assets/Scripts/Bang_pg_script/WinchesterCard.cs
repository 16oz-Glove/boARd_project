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
        stateName = "Rifle_idle";
    }

    protected override void Update_Player()
    {
        UIManager.instance.winchester_img.SetActive(true);  //윈체스터 이미지 활성화
        UIManager.instance.Winchester_Evet();
    }

}
