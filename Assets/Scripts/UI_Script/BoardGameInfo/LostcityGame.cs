using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LostcityGame : BoardGame_info
{
    protected override void Update_board_Name()
    {
        board_Name = "로스트 시티";
    }

    protected override void Update_ex()
    {
        ex = "\"투자에는 대가가 따르는 법\"";
    }

    protected override void Update_people()
    {
        people = "2명";
    }

    protected override void Update_time()
    {
        time = "평균 20분 소요";
    }

    protected override void Update_rank()
    {
        rank = "보통";
    }

    protected override void Update_age()
    {
        age = "만 8세 이상";
    }

    protected override void Update_Button()
    {
        button_st = board_Name + " 튜토리얼 시작";
        button_st_pg = board_Name + " 연습게임 시작";
    }

}
