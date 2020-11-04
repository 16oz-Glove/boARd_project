using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BangGame : BoardGame_info
{
    public new void Update_board_Name()
    {
        board_Name_Text.GetComponent<Text>().text = "뱅! Bang!";
    }

    public new void Update_ex()
    {
        ex_Text.GetComponent<Text>().text = "\"누구도 믿지마!\"";
    }

    public new void Update_people()
    {
        people.GetComponent<Text>().text = "최소 4~7명";
    }

    public new void Update_time()
    {
        time.GetComponent<Text>().text = "20~40분 소요";
    }

    public new void Update_rank()
    {
        rank.GetComponent<Text>().text = "어려움";
    }

    public new void Update_age()
    {
        age.GetComponent<Text>().text = "8세 이상";
    }

}
