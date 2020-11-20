using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PandemicGame : BoardGame_info
{
    protected override void Update_board_Name()
    {
        board_Name = "팬데믹";
    }

    protected override void Update_ex()
    {
        ex = "\"질병이 퍼지는걸 막아라!\"";
    }

    protected override void Update_people()
    {
        people = "2~4명";
    }

    protected override void Update_time()
    {
        time = "평균 45분 소요";
    }

    protected override void Update_rank()
    {
        rank = "매우 어려움";
    }

    protected override void Update_age()
    {
        age = "10세 이상";
    }

    protected override void Update_Button()
    {
        //만약 ARrecognize_pg Scene면
        if (SceneManager.GetActiveScene().name == "ARrecognize_pg")
        {
            button_st = board_Name + " 연습게임 시작";
        }
        else    // if(SceneManager.GetActiveScene().name == "ARrecognize") : 만약 ARrecognize_tt Scene면
        {
            button_st = board_Name + " 튜토리얼 시작";
        }

    }

}
