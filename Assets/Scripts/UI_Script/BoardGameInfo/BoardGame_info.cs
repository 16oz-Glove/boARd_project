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
    protected string button_st;         //튜토리얼 버튼의 Text를 넣어줄 string변수
    protected string button_st_pg;      //연습게임 버튼의 Text를 넣어줄 string변수

    private string sceneName;

    private GameObject boardName_gb;
    private GameObject ex_gb;
    private GameObject people_gb;
    private GameObject time_gb;
    private GameObject rank_gb;
    private GameObject age_gb;
    private GameObject Button_tt;       //튜토리얼 버튼 속에있는 텍스트오브젝트 변수
    private GameObject Button_pg;       //연습게임 버튼 속에있는 텍스트오브젝트 변수
    private GameObject Button_temp_tt;       //튜토리얼 버튼 오브젝트 저장변수
    private GameObject Button_temp_pg;       //연습게임 버튼 오브젝트 저장변수
    private Button Button_action_tt;       //튜토리얼 버튼 
    private Button Button_action_pg;       //연습게임 버튼 

    private Text BoardNam_Text;
    private Text ex_Text;
    private Text people_Text;
    private Text time_Text;
    private Text rank_Text;
    private Text age_Text;
    private Text Button1_Text;       //튜토리얼 버튼 속에있는 텍스트 변수
    private Text Button2_Text;       //튜토리얼 버튼 속에있는 텍스트 변수

    void Awake()
    {
        boardName_gb = GameObject.Find("BoardNam_Text").gameObject;
        ex_gb = GameObject.Find("ex_Text").gameObject;
        people_gb = GameObject.Find("An_people_Text").gameObject;
        time_gb = GameObject.Find("An_time_Text").gameObject;
        rank_gb = GameObject.Find("An_rank_Text").gameObject;
        age_gb = GameObject.Find("An_age_Text").gameObject;


        Button_tt = GameObject.Find("Button1_Text").gameObject;
        Button_pg = GameObject.Find("Button2_Text").gameObject;

        Button1_Text = Button_tt.GetComponent<Text>();
        Button2_Text = Button_pg.GetComponent<Text>();

        //"튜토리얼"버튼 가져오기
        Button_temp_tt = GameObject.Find("Start_Button").gameObject;
        Button_action_tt = Button_temp_tt.GetComponent<Button>();

        //"연습게임"버튼 가져오기
        Button_temp_pg = GameObject.Find("pgStart_Button").gameObject;
        Button_action_pg = Button_temp_pg.GetComponent<Button>();

        BoardNam_Text = boardName_gb.GetComponent<Text>();
        ex_Text = ex_gb.GetComponent<Text>();
        people_Text = people_gb.GetComponent<Text>();
        time_Text = time_gb.GetComponent<Text>();
        rank_Text = rank_gb.GetComponent<Text>();
        age_Text = age_gb.GetComponent<Text>();


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
        Button1_Text.text = button_st;
        Button2_Text.text = button_st_pg;

        BoardName.Name_Scene = sceneName;   //진행하려는 보드게임의 씬 이름

        Button_action_tt.interactable = true;
        Button_action_pg.interactable = true;
    }

}
