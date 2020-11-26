using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSet : MonoBehaviourPun
{
    private string job;       //플레이어 직업 
    private string Character;  //플레이어 캐릭터 카드
    private string Character_ability;   //플레이어 캐릭터 카드의 효과

    [Header("보안관 이미지")]
    public GameObject Sceriffo_card2;    //보안관 카드

    //각 플레이어 까지의 거리
    private int[] Player_far = new int[5];

    //생명력
    private int life;

    //자신의 차례인지 아닌지 구분해주는 bool변수
    private bool myTurn;

    [Header("플레이어의 PhotonView")]
    public PhotonView PV;

    [Header("자신의 닉네임 텍스트")]
    public Text NickNameText;

    void Awake()
    {
        //차례 리셋
        myTurn = false;
        //사용자 닉네임과 닉네임 색깔 넣어주기.
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;

    }

    // Start is called before the first frame update
    void Start()
    {
        //만약 방장이면,
        if (PhotonNetwork.IsMasterClient)
        {
            job = "보안관";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Sceriffo_img").gameObject.SetActive(true); // 보안관 카드 화면에 띄워주기
            life = 5;
            Character = "주르도네";
            Character_ability = "뱅의 표적이 될 때마다 카드 펼치기. 이때 하트가 나오면 총알 빗나감";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Jurdona_img").gameObject.SetActive(true);// 주르도네 화면에 띄워주기
            Sceriffo_card2.SetActive(true); //보안관 이미지 미니맵에 띄워지기
            Player_far[0] = 0;
            Player_far[1] = 1;
            Player_far[2] = 2;
            Player_far[3] = 2;
            Player_far[4] = 1;
        }
        else if ((int)(PV.ViewID / 1000) == 2)     //두번째 플레이어이면
        {
            job = "배신자";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Rinnegato_img").gameObject.SetActive(true); //배신자 화면에 띄워주기
            life = 4;
            Character = "별쳐 샘";
            Character_ability = "게임에서 제거되는 인물이 생길때마다 그 사람의 모든 카드 가져옴.";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Vulture_img").gameObject.SetActive(true); //벌쳐 샘 화면에 띄워주기
            Player_far[0] = 1;
            Player_far[1] = 0;
            Player_far[2] = 1;
            Player_far[3] = 2;
            Player_far[4] = 2;
        }
        else if ((int)(PV.ViewID / 1000) == 3)     //세번째 플레이어이면
        {
            job = "부관";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Vice_img").gameObject.SetActive(true); //부관 화면에 띄워주기
            life = 3;
            Character = "엘 그링고";
            Character_ability = "생명력 1 잃을때 마다 공격한 사람의 손에서 카드 한 장 가져옴.";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("El_img").gameObject.SetActive(true);// 엘 그링고 화면에 띄워주기
            Player_far[0] = 2;
            Player_far[1] = 1;
            Player_far[2] = 0;
            Player_far[3] = 1;
            Player_far[4] = 2;
        }
        else if ((int)(PV.ViewID / 1000) == 4)     //네번째 플레이어이면
        {
            job = "무법자";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Fuorilegge_img").gameObject.SetActive(true); //무법자 카드 화면에 띄워주기
            life = 4;
            Character = "윌리 더 키드";
            Character_ability = "뱅 카드를 원하는 만큼 사용";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Willy_img").gameObject.SetActive(true); //윌리 더 키드 화면에 띄워주기
            Player_far[0] = 2;
            Player_far[1] = 2;
            Player_far[2] = 1;
            Player_far[3] = 0;
            Player_far[4] = 1;
        }
        else if ((int)(PV.ViewID / 1000) == 5)     //다섯번째 플레이어이면
        {
            job = "무법자";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_job").transform.Find("job_Panel").transform.Find("Fuorilegge_img").gameObject.SetActive(true); //무법자 카드 화면에 띄워주기
            life = 4;
            Character = "시드 캐첨";
            Character_ability = "카드 두 장을 버려 생명력 +1 회복";
            GameObject.Find("Canvas").transform.Find("Panel").transform.Find("minimumButton_character").transform.Find("character_Panel").transform.Find("Sid_img").gameObject.SetActive(true);
            Player_far[0] = 1;
            Player_far[1] = 2;
            Player_far[2] = 2;
            Player_far[3] = 1;
            Player_far[4] = 0;
        }
        else Debug.Log("ViewID를 찾을수 없습니다.");

        //각 사용자와 관련된 화면을 띄워주기
        UpdateUI();
        UpdateUI_Player_info();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        //생명력 uI 수정해주기
        UIManager.instance.Update_Life(life);

        //거리 UI 수정해주기
        UIManager.instance.Update_Far(Player_far);
    }

    public void UpdateUI_Player_info()
    {
        //생명력 uI 수정해주기
        UIManager.instance.Update_PlayerInfo(job, Character, Character_ability);
    }

    //생명력 빼주기
    public void Minus_life(int life_num)
    {
        life -= life_num;
    }

    //생명력 플러스 해주기
    public void Plus_life(int life_num)
    {
        life += life_num;
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



}
