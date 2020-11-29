using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mirino_card : GameCard     //조준경 카드
{
    protected void update_Object()
    {
        //game_obj = gameObject;
    }

    public override void Effect()
    {
        //int i = (int)(playerSet.PV.ViewID / 1000);

        //switch (i)  //다른사람이 보는 나의 거리 1 늘리기
        //{
        //    case 1: //내가 첫번째 플레이어라면(방장이라면)
        //        if (i == 2) //나머지 플레이어는?????????????????
        //            playerSet.Plus_far(1, 1);   //나 사이의 거리가 1 증가
        //        break;
        //}



        //for (i = 0; i < 5; i++)
        //    far[i] = playerSet.Getfar(i + 1);

        //instance.Update_Far(far);



        base.Effect();
        Debug.Log("조준경 카드 효과 발동");
    }


}
