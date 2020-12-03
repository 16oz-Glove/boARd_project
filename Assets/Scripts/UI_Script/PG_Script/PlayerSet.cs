using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSet : MonoBehaviourPun
{
    public string job;       //플레이어 직업 
    public string Character;  //플레이어 캐릭터 카드
    public string Character_ability;   //플레이어 캐릭터 카드의 효과
    public int[] Player_far = new int[5];    //각 플레이어 까지의 거리
    private GameObject my; //플레이어 버튼을 가리키는 변수

    //생명력
    private int life;

    //자신의 차례인지 아닌지 구분해주는 bool변수
    private int myTurn;

    [Header("플레이어의 PhotonView")]
    public PhotonView PV;

    [Header("플레이어들의 버튼")]
    public GameObject[] myButton = new GameObject[5];    //동기화가 안되고 있는 Object

    void Awake()
    {
        //차례 리셋
        myTurn = PhotonNetwork.LocalPlayer.ActorNumber;
    }

    void Start()
    {
        set_my_PlayerButton(PhotonNetwork.LocalPlayer.ActorNumber);
        SetPlayer_Button_info();
        if (PhotonNetwork.IsMasterClient)
        {
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Sceriffo_img").gameObject.SetActive(true); // 보안관 카드 화면에 띄워주기
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Jurdona_img").gameObject.SetActive(true);// 주르도네 화면에 띄워주기
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 2)     //두번째 플레이어이면
        {
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Rinnegato_img").gameObject.SetActive(true); //배신자 화면에 띄워주기
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Vulture_img").gameObject.SetActive(true); //벌쳐 샘 화면에 띄워주기
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 3)     //세번째 플레이어이면
        {
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Vice_img").gameObject.SetActive(true); //부관 화면에 띄워주기
            Character_ability = "생명력 1 잃을때 마다 공격한 사람의 손에서 카드 한 장 가져옴.";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("El_img").gameObject.SetActive(true);// 엘 그링고 화면에 띄워주기
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 4)     //네번째 플레이어이면
        {
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Fuorilegge_img").gameObject.SetActive(true); //무법자 카드 화면에 띄워주기
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Willy_img").gameObject.SetActive(true); //윌리 더 키드 화면에 띄워주기
        }
        else if (PhotonNetwork.LocalPlayer.ActorNumber == 5)     //다섯번째 플레이어이면
        {
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Fuorilegge_img").gameObject.SetActive(true); //무법자 카드 화면에 띄워주기
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Sid_img").gameObject.SetActive(true);
        }
        else Debug.Log("ViewID를 찾을수 없습니다.");


        //각 사용자와 관련된 화면을 띄워주기
        UpdateUI_Life();
        UpdateUI_Far();
        UpdateUI_Player_info();
        UIManager.instance.SetPlayer(PhotonNetwork.LocalPlayer.ActorNumber);    //플레이어들의 버튼 업데이트 하기
        UIManager.instance.reName(PhotonNetwork.LocalPlayer.ActorNumber);       //플레이어들의 닉네임 업데이트
    }

    //자신을 가리키는 버튼
    public void set_my_PlayerButton(int ActorNum)
    {
        my =  myButton[ActorNum-1];
    }

    //자신을 가리키는 버튼
    public GameObject get_my_PlayerButton()
    {
        return my;
    }


    public void UpdateUI_Far()
    {
        //거리 UI 수정해주기
        UIManager.instance.Update_Far(my.GetComponent<PlayerButton>().Player_far);
    }

    public void UpdateUI_Life()
    {
        //생명력 uI 수정해주기
        UIManager.instance.Update_Life(my.GetComponent<PlayerButton>().life);
    }

    public void UpdateUI_Player_info()
    {
        //직업, 캐릭터, 캐릭터 능력 화면에 업로드
        UIManager.instance.Update_PlayerInfo(my.GetComponent<PlayerButton>().job, my.GetComponent<PlayerButton>().Character, my.GetComponent<PlayerButton>().Character_ability);
    }

    //생명력 빼주기
    public void Minus_life(int life_num)
    {
        my.GetComponent<PlayerButton>().life = my.GetComponent<PlayerButton>().life - life_num;
        UpdateUI_Life();
    }

    //생명력 플러스 해주기
    public void Plus_life(int life_num)
    {
        my.GetComponent<PlayerButton>().life = my.GetComponent<PlayerButton>().life + life_num;
        UpdateUI_Life();
    }

    //본인과 떨어진 거리를 반환
    public int Getfar(int my_selfnum)
    {
        return Player_far[my_selfnum - 1];
    }

    //본인과의 거리 빼주기
    public void Minus_far(int my_selfnum, int far)
    {
        Player_far[my_selfnum - 1] -= far;
    }

    //본인과의 거리 플러스 해주기
    public void Plus_far(int my_selfnum, int far)
    {
        Player_far[my_selfnum - 1] += far;
    }

    //조준경 사용했을때!
    public void Minus_Far_All()
    {
        for(int i = 0;i <= my.GetComponent<PlayerButton>().Player_far.Length-1; i++)
        {
            my.GetComponent<PlayerButton>().Player_far[i]--;
            if (my.GetComponent<PlayerButton>().Player_far[i] < 0) my.GetComponent<PlayerButton>().Player_far[i] = 0;
        }
    }


    //각 버튼들의 정보 넣어주기
    private void SetPlayer_Button_info()
    {
        myButton[0].GetComponent<PlayerButton>().job = "보안관";
        myButton[0].GetComponent<PlayerButton>().Character = "주르도네";
        myButton[0].GetComponent<PlayerButton>().Character_ability = "뱅의 표적이 될 때마다 카드 펼치기. 이때 하트가 나오면 총알 빗나감";
        myButton[0].GetComponent<PlayerButton>().life = 5;
        myButton[0].GetComponent<PlayerButton>().Player_far[0] = 0;
        myButton[0].GetComponent<PlayerButton>().Player_far[1] = 1;
        myButton[0].GetComponent<PlayerButton>().Player_far[2] = 2;
        myButton[0].GetComponent<PlayerButton>().Player_far[3] = 2;
        myButton[0].GetComponent<PlayerButton>().Player_far[4] = 1;

        myButton[1].GetComponent<PlayerButton>().job = "배신자";
        myButton[1].GetComponent<PlayerButton>().Character = "벌쳐 샘";
        myButton[1].GetComponent<PlayerButton>().Character_ability = "생명력 1 잃을때 마다 공격한 사람의 손에서 카드 한 장 가져옴.";
        myButton[1].GetComponent<PlayerButton>().life = 4;
        myButton[1].GetComponent<PlayerButton>().Player_far[0] = 1;
        myButton[1].GetComponent<PlayerButton>().Player_far[1] = 0;
        myButton[1].GetComponent<PlayerButton>().Player_far[2] = 1;
        myButton[1].GetComponent<PlayerButton>().Player_far[3] = 2;
        myButton[1].GetComponent<PlayerButton>().Player_far[4] = 2;

        myButton[2].GetComponent<PlayerButton>().job = "부관";
        myButton[2].GetComponent<PlayerButton>().Character = "엘그링고";
        myButton[2].GetComponent<PlayerButton>().Character_ability = "게임에서 제거되는 인물이 생길때마다 그 사람의 모든 카드 가져옴.";
        myButton[2].GetComponent<PlayerButton>().life = 3;
        myButton[2].GetComponent<PlayerButton>().Player_far[0] = 2;
        myButton[2].GetComponent<PlayerButton>().Player_far[1] = 1;
        myButton[2].GetComponent<PlayerButton>().Player_far[2] = 0;
        myButton[2].GetComponent<PlayerButton>().Player_far[3] = 1;
        myButton[2].GetComponent<PlayerButton>().Player_far[4] = 2;

        myButton[3].GetComponent<PlayerButton>().job = "무법자";
        myButton[3].GetComponent<PlayerButton>().Character = "윌리 더 키드";
        myButton[3].GetComponent<PlayerButton>().Character_ability = "뱅 카드를 원하는 만큼 사용";
        myButton[3].GetComponent<PlayerButton>().life = 4;
        myButton[3].GetComponent<PlayerButton>().Player_far[0] = 2;
        myButton[3].GetComponent<PlayerButton>().Player_far[1] = 2;
        myButton[3].GetComponent<PlayerButton>().Player_far[2] = 1;
        myButton[3].GetComponent<PlayerButton>().Player_far[3] = 0;
        myButton[3].GetComponent<PlayerButton>().Player_far[4] = 1;

        myButton[4].GetComponent<PlayerButton>().job = "무법자";
        myButton[4].GetComponent<PlayerButton>().Character = "시드 캐첨";
        myButton[4].GetComponent<PlayerButton>().Character_ability = "카드 두 장을 버려 생명력 +1 회복";
        myButton[4].GetComponent<PlayerButton>().life = 4;
        myButton[4].GetComponent<PlayerButton>().Player_far[0] = 1;
        myButton[4].GetComponent<PlayerButton>().Player_far[1] = 2;
        myButton[4].GetComponent<PlayerButton>().Player_far[2] = 2;
        myButton[4].GetComponent<PlayerButton>().Player_far[3] = 1;
        myButton[4].GetComponent<PlayerButton>().Player_far[4] = 0;
    }

}
