using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardGame_info : MonoBehaviour
{
    protected string board_Name;
    protected string ex;
    protected string people;
    protected string time;
    protected string rank;
    protected string age;
    protected string button_st;

    private string sceneName;

    private GameObject boardName_gb;
    private GameObject ex_gb;
    private GameObject people_gb;
    private GameObject time_gb;
    private GameObject rank_gb;
    private GameObject age_gb;
    private GameObject Button;
    private GameObject Button_temp;
    private Button Button_action;

    private Text BoardNam_Text;
    private Text ex_Text;
    private Text people_Text;
    private Text time_Text;
    private Text rank_Text;
    private Text age_Text;
    private Text Button_Text;

    void Awake()
    {
        boardName_gb = GameObject.Find("BoardNam_Text").gameObject;
        ex_gb = GameObject.Find("ex_Text").gameObject;
        people_gb = GameObject.Find("An_people_Text").gameObject;
        time_gb = GameObject.Find("An_time_Text").gameObject;
        rank_gb = GameObject.Find("An_rank_Text").gameObject;
        age_gb = GameObject.Find("An_age_Text").gameObject;
        Button = GameObject.Find("Button_Text").gameObject;

        //"연습게임"버튼 가져오기
        Button_temp = GameObject.Find("Start_Button").gameObject;
        Button_action = Button_temp.GetComponent<Button>();

        BoardNam_Text = boardName_gb.GetComponent<Text>();
        ex_Text = ex_gb.GetComponent<Text>();
        people_Text = people_gb.GetComponent<Text>();
        time_Text = time_gb.GetComponent<Text>();
        rank_Text = rank_gb.GetComponent<Text>();
        age_Text = age_gb.GetComponent<Text>();
        Button_Text = Button.GetComponent<Text>();

    }

    void Start()
    {
        Update_board_Name();
        Update_ex();
        Update_people();
        Update_time();
        Update_rank();
        Update_age();
        Update_Button();

        sceneName = this.gameObject.name;   //씬 이름과 보드 오브젝트의 이름이 같다.
    }

    protected virtual void Update_board_Name()
    {

    }

    protected virtual void Update_ex()
    {

    }

    protected virtual void Update_people()
    {

    }

    protected virtual void Update_time()
    {

    }

    protected virtual void Update_rank()
    {

    }

    protected virtual void Update_age()
    {

    }

    protected virtual void Update_Button()
    {

    }

    //오브젝트 터치가 났을때, 서브패널 업로드 시킴.
    public void Touching_Board()
    {
        BoardNam_Text.text = board_Name;
        ex_Text.text = ex;
        people_Text.text = people;
        time_Text.text = time;
        rank_Text.text = rank;
        age_Text.text = age;
        Button_Text.text = button_st;

        Button_action.interactable = true;
    }

}
