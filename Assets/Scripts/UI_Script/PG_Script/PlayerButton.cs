using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;  //MonoBehaviourPunCallbacks 상속을 위한 using

public class PlayerButton : MonoBehaviourPun
{
    //사용자와 사용자 버튼와 연결된 스크립트. RPC이용을 위해 이어준다
    public PlayerSet useforRPC;

    public string job;                  //플레이어 직업 
    public string Character;            //플레이어 캐릭터 카드
    public int life;                    //생명력
    public string Character_ability;    //플레이어 캐릭터 카드의 효과
    public int[] Player_far = new int[5];    //각 플레이어 까지의 거리


    void start()
    {

    }

    //뱅에 맞았을때 실행되게 하기
    public void BangTarget_Player()
    {
        if (UIManager.instance.Player_Turn == 1)
        {
            useforRPC.Minus_life(1);        //뱅에 맞았으므로, 생명력 줄여주고
        }
        else if (UIManager.instance.Player_Turn == 2)
        {
            useforRPC.Minus_life(1);
            UIManager.instance.Player2_Mirino();
        }
        else return;
    }

    public void click_Button()
    {
        UIManager.instance.Player1_To_Player5();
    }

}
