using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardGame_info : MonoBehaviour
{
    protected Text board_Name_Text;
    protected Text ex_Text;
    protected Text people;
    protected Text time;
    protected Text rank;
    protected Text age;

    public string sceneName;

    void Start()
    {
        Update_board_Name();
        Update_ex();
        Update_people();
        Update_time();
        Update_rank();
        Update_age();

    }

    protected void Update_board_Name()
    {

    }

    protected void Update_ex()
    {

    }

    protected void Update_people()
    {

    }

    protected void Update_time()
    {

    }

    protected void Update_rank()
    {

    }

    protected void Update_age()
    {

    }

    public Text Get_board_Name()
    {
        return board_Name_Text;
    }

    public Text Get_ex()
    {
        return ex_Text;
    }

    public Text Get_people()
    {
        return people;
    }

    public Text Get_time()
    {
        return time;
    }

    public Text Get_rank()
    {
        return rank;
    }

    public Text Get_age()
    {
        return age;
    }

}
