using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prigione_card : GameCard     //감옥 카드
{
    protected void update_Object()
    {
        //game_obj = gameObject;
    }

    public override void Effect()
    {
        base.Effect();
        Debug.Log("조준경 카드 효과 발동");
    }
}
