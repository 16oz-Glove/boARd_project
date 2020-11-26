using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSet : MonoBehaviourPun
{
    [Header("플레이어의 직업")]
    //직업 이름
    public Text jobName;

    [Header("플레이어의 직업카드별 저장 변수")]
    //직업카드 저장할 변수
    public GameObject Vice_card;        //부관 카드
    public GameObject Rinnegato_card;   //배신자 카드
    public GameObject Sceriffo_card;    //보안관 카드
    public GameObject Sceriffo_card2;    //보안관 카드
    public GameObject Fuorilegge_card;  //무법자 카드

    //캐릭터 이름
    private string characterName;
    //생명력
    private int life;
    //자신의 차례인지 아닌지 구분해주는 bool변수
    private bool myTurn;

    //생명력 이미지 저장할 변수
    public GameObject[] life_img = new GameObject[5];


    [Header("플레이어의 PhotonView")]
    public PhotonView PV;

    [Header("자신의 닉네임 텍스트")]
    public Text NickNameText;

    [Header("자신의 캐릭터 이미지")]
    public Image myCharacter_img;

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
            jobName.text = "보안관";
            life = 5;
            UpdateCharacter("");
            Sceriffo_card.SetActive(true); //보안관 이미지 화면에 띄워주기
            Sceriffo_card2.SetActive(true); //보안관 이미지 미니맵에 띄워지기
        }
        else if(PV.ViewID == 2)     //두번째 플레이어이면
        {
            jobName.text = "부관";
            life = 4;
            UpdateCharacter("");
            Vice_card.SetActive(true); //부관 이미지 화면에 띄워주기
        }
        else if (PV.ViewID == 3)     //세번째 플레이어이면
        {
            jobName.text = "무법자";
            life = 4;
            UpdateCharacter("");
            Fuorilegge_card.SetActive(true); //무법자 이미지 화면에 띄워주기
        }
        else if (PV.ViewID == 4)     //네번째 플레이어이면
        {
            jobName.text = "배신자";
            life = 4;
            UpdateCharacter("");
            Rinnegato_card.SetActive(true); //배신자 이미지 화면에 띄워주기
        }
        else if (PV.ViewID == 5)     //다섯번째 플레이어이면
        {
            jobName.text = "무법자";
            life = 4;
            UpdateCharacter("");
            Fuorilegge_card.SetActive(true); //무법자 이미지 화면에 띄워주기
        }

        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateUI()
    {
        //생명력 uI 수정해주기
        for (int i = 0; i <=life-1; i++)//생명력 만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(true);
        }
        for(int i = life-1 ; i < 4; i++)//생명력 깍인만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(false);
        }
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

    //캐릭터 넣어주기
    public void UpdateCharacter(string character)
    {
        characterName = character;
    }



}
