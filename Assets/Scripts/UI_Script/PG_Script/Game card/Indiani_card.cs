using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indiani_card : GameCard     //인디언 카드
{
    protected void update_Object()
    {
        //game_obj = gameObject;
    }

    public override void Effect()
    {
        base.Effect();
        Debug.Log("인디언 카드 효과 발동");
    }
}
