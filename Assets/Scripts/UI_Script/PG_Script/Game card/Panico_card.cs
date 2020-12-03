using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panico_card : GameCard     //강탈 카드
{
    protected void update_Object()
    {
        //game_obj = gameObject;
    }

    public override void Effect()
    {
        base.Effect();
        Debug.Log("강탈 카드 효과 발동");
        minimap.SetActive(true);
        UIManager.instance.Player1_Turn2_Text();
    }
}
