using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatBalou_card : GameCard     //캣벌로우 카드
{
    protected void update_Object()
    {
        //game_obj = gameObject;
    }

    public override void Effect()
    {
        base.Effect();
        Debug.Log("캣벌로우 카드 효과 발동");
    }
}
