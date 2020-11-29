using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class UIManager : MonoBehaviourPun
{ 
    //UIManager 타입의 오브젝트를 다른 스크립트에서 즉시 접근할 수 있또록 정적 프로퍼티 instance와 정적 변수 m_instance로 싱글턴으로 구현
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }
    private static UIManager m_instance;

    [Header("각 플레이어까지의 거리를 나타내는 텍스트")]
    public Text[] Player_far_text = new Text[5];

    [Header("생명력 이미지 저장 변수")]
    public GameObject[] life_img = new GameObject[5];

    [Header("플레이어 직업과 캐릭터 텍스트 변수")]
    public Text jobName;       //플레이어 직업 
    public Text CharacterName;  //플레이어 캐릭터 카드
    public Text ability;   //플레이어 캐릭터카드의 효과

    [Header("사용자들의 위치를 나타내는 버튼")]
    public GameObject[] Player_Button = new GameObject[5];
    public GameObject Sceriffo_card_img;

    [Header("사용자들의 닉네임 텍스트")]
    public Text[] NickNameText = new Text[5];

    [Header("미니맵 가운데 알림 텍스트")]
    public Text Anounce;    // 미니맵 중앙에 알림 표시할 부분

    [Header("사용자 턴")]
    public int Player_Turn = 1;

    [Header("사용자 턴을 넘기라는 메시지")]
    public GameObject check_Qution;

    //턴 넘기기(버튼 눌렀을때)
    public void NextTurn()
    {
        photonView.RPC("NextTurnRPC", RpcTarget.All);
    }

    //사용자들 닉네임 업데이트 시키기
    public void reName(int ActorNumber)
    {
        photonView.RPC("reNameRPC",RpcTarget.AllBuffered, ActorNumber);
    }

    [PunRPC]
    private void reNameRPC(int ActorNumber)
    {
        //사용자 닉네임과 닉네임 색깔 넣어주기.
        for(int i = (ActorNumber - 1); i >= 0; i--)
        {
            NickNameText[i].text = PhotonNetwork.PlayerList[i].NickName;
            NickNameText[i].color = Color.green;
        }
    }

    //플레이어들의 버튼들 업데이트 하기
    public void SetPlayer(int ActorNumber)
    {
        //플레이어들의 버튼 업데이트 하기
        photonView.RPC("Set_Player_buttonRPC", RpcTarget.AllBuffered, ActorNumber);
    }

    public void SetAnounce(string anounce)
    {
        Anounce.text = anounce;
    }

    [PunRPC]
    private void Set_Player_buttonRPC(int ActorNumber)
    {
        int i = ActorNumber - 1;
        while (i >= 0)
        {
            //방장 플레이어 세팅!
            Player_Button[i].SetActive(true);                                     //버튼 활성화 시키고
            if (i == 0)
            {
                Player_Button[i].GetComponent<Button>().image.color = Color.red;    //버튼색 빨간색으로!
                Sceriffo_card_img.SetActive(true);                               //보안관 이미지 띄워주기
                break;
            }
            else if (i == 4)    // 다섯번째 플레이어 버튼 색 바꾸기
            {
                Player_Button[i].GetComponent<Button>().image.color = Color.cyan;    //버튼색 변경!
            }
            else if (i == 3)    // 네번째 플레이어 버튼 색 바꾸기
            {
                Player_Button[i].GetComponent<Button>().image.color = Color.green;    //버튼색 변경!
            }
            else if (i == 2)    // 세번째 플레이어 버튼 색 바꾸기
            {
                Player_Button[i].GetComponent<Button>().image.color = Color.yellow;    //버튼색 변경!
            }
            else if (i == 1)    // 두번째 플레이어 버튼 색 바꾸기
            {
                Player_Button[i].GetComponent<Button>().image.color = Color.blue;    //버튼색 변경!
            }
            i--;
        }
    }

    public void Update_Life(int life)
    {
        for (int i = 0; i <= life - 1; i++)//생명력 만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(true);
        }
        for (int i = life; i < 5; i++)//생명력 깍인만큼 총알UI 띄워주기
        {
            life_img[i].SetActive(false);
        }
    }

    public void Update_Far(int[] far)
    {
        for (int i = 0; i < 5; i++)
        {
            Player_far_text[i].text = far[i].ToString();
        }
    }

    public void Update_PlayerInfo(string job, string character,string character_ab)
    {
        jobName.text = job;     //직업
        CharacterName.text = character; //캐릭터
        ability.text = character_ab;    //캐릭터의 능력
    }

    public void set_notice_text(string notice)
    {
        photonView.RPC("Set_noticeRPC",RpcTarget.All, notice);
    }

    public void set_notice_Turn(int playerTurn)
    {
        photonView.RPC("setAnounce_TurnRPC", RpcTarget.All, playerTurn);
    }

    //차례를 끝내기겠습니까 라는 UI 띄우기
    public void Active_NextTurn()
    {
        check_Qution.SetActive(true);
    }

    [PunRPC]
    private void setAnounce_TurnRPC(int playerTurn)
    {
        set_notice_text(PhotonNetwork.PlayerList[playerTurn-1].NickName + "의 차례입니다.");
    }

    [PunRPC]
    private void Set_noticeRPC(string notice)
    {
        Anounce.text = notice;
    }

    [PunRPC]
    private void NextTurnRPC()
    {
        if (Player_Turn == 5)
        {
            Player_Turn = 1;
        }
        else Player_Turn++;

        set_notice_text(Player_Button[Player_Turn-1].GetComponent<PlayerButton>().Character + "차례입니다.");
    }


}
