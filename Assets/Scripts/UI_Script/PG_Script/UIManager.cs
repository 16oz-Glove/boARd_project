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
    public bool turn = false;

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

    [Header("사용자 상태/명력 텍스트")]
    public Text State_text;    // 미니맵 중앙에 알림 표시할 부분

    [Header("사용자 턴")]
    public int Player_Turn = 1;

    [Header("사용자 턴을 넘기라는 메시지")]
    public GameObject check_Qution;

    [Header("빗나감 카드를 가지고 계시냐를 물어보는 패널")]
    public GameObject check_Mancato;

    [Header("카드 한장을 버리라는 패널")]
    public GameObject check_catbalrow;

    [Header("조준경 이미지")]
    public GameObject mirino;

    [Header("인디언 카드 ")]
    public GameObject indion_Panel;

    [Header("감옥 카드 오브젝트")]
    public GameObject prision;

    [Header("감옥 카드 패널")]
    public GameObject prision_Panel;

    [Header("감옥 카드 패널2")]
    public GameObject prision_Panel2;

    [Header("감옥 카드 이미지")]
    public GameObject prision_img;

    [Header("조준경 패널")]
    public GameObject mirino_Panel;

    [Header("맥주 패널")]
    public GameObject Rirra_Panel;

    [Header("결투 패널")]
    public GameObject Duo_Panel;

    [Header("윈체스터 이미지")]
    public GameObject winchester_img;

    //턴 넘기기(버튼 눌렀을때)
    public void NextTurn()
    {
        State_text.text = "차례를 넘깁니다.";
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

    public void Update_LifeRPC_use(int life)
    {
        photonView.RPC("Update_Life_RPC",RpcTarget.All,life);
    }

    [PunRPC]
    private void Update_Life_RPC(int life)
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

    //차례를 끝내시겠습니까 라는 UI 띄우기
    public void Active_NextTurn()
    {
        check_Qution.SetActive(true);
    }

    [PunRPC]
    private void setAnounce_TurnRPC(int playerTurn)
    {
        Anounce.text = PhotonNetwork.PlayerList[playerTurn-1].NickName + "님의 차례입니다.";
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

        Anounce.text = PhotonNetwork.PlayerList[Player_Turn - 1].NickName + "차례입니다.";

        //차례3 일때,
        if (Player_Turn == 3)
        {
            photonView.RPC("Player_Action_RPC", RpcTarget.All, "카드 한장을 뽑아주세요.\n" + PhotonNetwork.PlayerList[3].NickName + "님에게 \"뱅\"카드를 사용해보겠습니다.\n뱅카드를 인식하고 터치해 주세요", PhotonNetwork.PlayerList[2].NickName);
        }
    }

    public void Player_Action__State_Please(string str)
    {
        State_text.text = str;
    }

    public void Player1_Turn1_Text()
    {
        State_text.text = "카드더미에서 한장을 뽑겠습니다.\n\"강탈\" 카드를 인식시켜 터치한 후, 사용하도록 하겠습니다.";
    }

    public void Player1_Turn2_Text()
    {
        State_text.text = "바로 오른쪽 사람에게 \"강탈\"카드를 사용해 보겠습니다.\n 미니맵에서 오른쪽 사용자의 오브젝트를 터치해 주세요.";

    }

    //사용자(E) 버튼에 넣을 메서드
    public void Player1_To_Player5()
    {
        if (turn)
        {
            check_Qution.SetActive(true);   //차례를 넘기겠습니다. 하는 패널 활성화
            photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[0].NickName + "님이 " + PhotonNetwork.PlayerList[4].NickName + "님에게\n\"뱅\" 카드를 사용하였습니다.\n");
            photonView.RPC("Player_Active_Mancato_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[4].NickName);  //빗나감 카드를 사용하시겠습니까? 하는 문구 띄워주기
            photonView.RPC("Player2_Start_RPC", RpcTarget.All); // 두번째 플레이어의 시작을 알림
        }
        else{
            photonView.RPC("Player1_To_Player5_RPC", RpcTarget.Others);  //RpcTarget.Others: 자기 빼고 모두 실행
            Player_Action__State_Please("다음은 감옥 카드를 사용해 보겠습니다. \n감옥 카드를 인식시키고 터치해주세요");
            turn = true;
        }
    }

    //감옥 사용을 위한 메서드
    public void Player1_To_Player4_Prision()
    {
        State_text.text = PhotonNetwork.PlayerList[3]+"님에게 사용해 보세요.";
    }

    public void Player3_to_Player4()
    {
        photonView.RPC("Player_Action_RPC", RpcTarget.All, "카드더미에서 카드 한장을 가져옵니다.\n 뱅 카드를 사용해 보겠습니다. \n뱅카드를 인식후 터치해 주세요.", PhotonNetwork.PlayerList[2].NickName);
        
    }

    public void Player3_To_Player4_Bang_Event()
    {
        Player_Action__State_Please(PhotonNetwork.PlayerList[3] + "님에게 뱅 카드를 사용해보세요.\n미니맵에서 사정거리 안에 있는 " + PhotonNetwork.PlayerList[3] + "님을 터치하세요.");
    }

    public void Player3_To_Player4_Mancato()
    {
        photonView.RPC("Player_Active_Mancato_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[3].NickName);  //빗나감 카드를 사용하시겠습니까? 하는 문구 띄워주기
    }

    public void BangTarget_Player4()
    {
        Player_Action__State_Please("빗나감 카드를 인식하고 터치해주세요");
    }

    //D가 빗나감 카드를 사용했을 경우, C 에게 인디언 카드 실행하라고 말한다.
    public void Player4_to_Mantaco()
    {
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.All, PhotonNetwork.PlayerList[3].NickName + "님이 빗나감 카드를 사용하였습니다.");
        photonView.RPC("Player_Action_RPC", RpcTarget.All, "이번에는 \"인디언\"카드를 사용해 보겠습니다.\n인식 후 터치하여 주세요.", PhotonNetwork.PlayerList[2].NickName);
    }

    //플레이어 1이 플레이어 4에게 감옥카드 사용할때와 플레이어 3이 4에게 뱅 카드를 사용할때, 버튼4 메서드
    public void Player1_To_Player4_and_Player3_To_Player4()
    {
        photonView.RPC("Player1_To_Player4_and_Player3_To_Player4_RPC", RpcTarget.Others);
        State_text.text = "다음은 뱅 카드를 사용해 보겠습니다.\n인식후 터치해 보세요.";
    }

    //사용자(E) 버튼에 넣을 메서드
    [PunRPC]
    private void Player1_To_Player5_RPC()
    {
        if (Player_Turn == 1)
        {
            State_text.text = PhotonNetwork.PlayerList[0].NickName + "님이 " + PhotonNetwork.PlayerList[4].NickName + "님에게\n\"강탈\" 카드를 사용하였습니다.\n" + PhotonNetwork.PlayerList[4].NickName + "님의 카드 하나를 가져옵니다.\n(연습게임이므로, 감옥 카드를 가져오시겠습니다!)";
        }
        else if (Player_Turn == 5)
        {
            State_text.text = PhotonNetwork.PlayerList[4].NickName + "님이 " + PhotonNetwork.PlayerList[3].NickName + "님에게\n\"결투\" 카드를 사용하였습니다.";
        }
        else return;

    }

    //사용자(D) 버튼에 넣을 메서드
    [PunRPC]
    private void Player1_To_Player4_and_Player3_To_Player4_RPC()
    {
        if (Player_Turn == 1)
        {
            State_text.text = PhotonNetwork.PlayerList[0].NickName + "님이 " + PhotonNetwork.PlayerList[3].NickName + "님에게\n\"감옥\" 카드를 사용하였습니다.\n";
            if(PhotonNetwork.PlayerList[3].NickName == PhotonNetwork.LocalPlayer.NickName) // 감옥 오브젝트 활성화 해주기.
            {
                prision.SetActive(true);
                prision_Panel.SetActive(true);
                prision_img.SetActive(true);
            }
        }
        else if (Player_Turn == 3)
        {
            Player3_To_Player4_Mancato();
        }
        else return;

    }

    //입력하는 문구 사용자 state 패널에 띄워주기
    [PunRPC]
    private void Player_Action_Please_All_RPC(string str)
    {
        State_text.text = str;
    }

    //입력한 문구와 사용자 이름 넣어주면 해당 사용자의 화면에 텍스트가 업데이트 된다.
    [PunRPC]
    private void Player_Action_RPC(string str, string name)
    {
        if(PhotonNetwork.LocalPlayer.NickName == name)
        {
            State_text.text = str;
        }
    }

    //빗나감 카드를 사용하시겠습니까? 하는 문구 띄워주기
    [PunRPC]
    private void Player_Active_Mancato_RPC(string name)
    {
        if (PhotonNetwork.LocalPlayer.NickName  == name)
            check_Mancato.SetActive(true);
    }

    [PunRPC]
    private void Player2_Start_RPC()
    {
        //두번째 플레이어라면
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[1].NickName)
        {
            State_text.text = "카드더미에서 카드 한장을 뽑으시겠습니다.\n캣벌로우 카드를 사용해 보겠습니다.\n카드를 인식 후 터치하여 주세요";
        }
    }

    [PunRPC]
    private void Player2_To_Player1_RPC()
    {
        //첫번째 플레이어라면
        if (PhotonNetwork.LocalPlayer.NickName == PhotonNetwork.PlayerList[0].NickName)
        {
            check_catbalrow.SetActive(true);
        }
        State_text.text = PhotonNetwork.PlayerList[1].NickName +"님이 " + PhotonNetwork.PlayerList[0].NickName + "님에게 \n\"캣벌로우\" 카드를 사용하였습니다.";
    }

    //사용자 2가 사용자 버튼 1을 눌렀을때(캣벌로우 카드를 A에게 썼을때 A에게 카드 한장을 버리라고 한다.)
    public void Player2_To_Player1_Catbalrow()
    {
        photonView.RPC("Player2_To_Player1_RPC",RpcTarget.Others);
        State_text.text = "다음은 뱅 카드를 사용해 보도록 하겠습니다.\n 뱅 카드를 인식후 터치하여 주세요.";
    }

    public void minino_button()
    {
        check_Qution.SetActive(true);
        Player3_to_Player4();
    }

    public void Player2_To_Player3_Bang()
    {
        State_text.text = PhotonNetwork.PlayerList[2].NickName +"님에게 \"뱅\" 카드를 사용해 보겠습니다.\n 미니맵에서"+ PhotonNetwork.PlayerList[2].NickName+ "님을 터치하여 주세요.";
    }

    public void Player3_Mantaco()
    {
        photonView.RPC("Player_Active_Mancato_RPC",RpcTarget.All,PhotonNetwork.PlayerList[2].NickName);
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others,PhotonNetwork.PlayerList[1].NickName + "님이 " + PhotonNetwork.PlayerList[2].NickName + "에게 뱅카드를 사용하였습니다.");
        Player2_Mirino();
    }

    public void Player2_Mirino()
    {
        photonView.RPC("Player_Action_RPC", RpcTarget.All,"이번에는 조준경 카드를 장착해 보겠습니다.\n조준경 카드를 인식후 터치해 주세요.",PhotonNetwork.PlayerList[1].NickName);
    }

    //str을 받아서 전체 사용자들에게 메세지를 전달
    public void Playe_All_state(string str)
    {
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, str);
    }

    public void Player2_All_state(string str)
    {
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[1].NickName + str);
    }

    public void Player3_To_Indian_all()
    {
        photonView.RPC("Player3_to_all_Indian_RPC", RpcTarget.Others);
        check_Qution.SetActive(true);   //  다음턴으로 넘기겠습니다.패널 띄우기
        photonView.RPC("Player4_Prigione_Card",RpcTarget.All);    //플레이어 4 화면에는 감옥카드 띄워주기
    }
    
    public void Player4_To_No_Button()
    {
        NextTurn();
        photonView.RPC("Player_Action_RPC", RpcTarget.All, "맥주 카드를 사용해 보겠습니다.\n맥주카드를 인식 후 터치해 주세요.", PhotonNetwork.PlayerList[4].NickName);
    }


    [PunRPC]
    private void Player4_Prigione_Card()
    {
        if (PhotonNetwork.PlayerList[3].NickName == PhotonNetwork.LocalPlayer.NickName)
            prision_Panel2.SetActive(true);
    }

    [PunRPC]
    private void Player3_to_all_Indian_RPC()
    {
        indion_Panel.SetActive(true);   //인디언 카드를 사용한다는 패널 띄워주기
    }

    public void Indian_card_no()
    {
        GetComponent<PlayerSet>().Minus_life(1);
    }


    public void Rirra_OK_Button()
    {
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[4].NickName + "님이 \n\"맥주\" 카드를 사용하였습니다.\n생명력이 1 증가합니다.");
        State_text.text = "결투카드를 사용해 보겠습니다.\n결투카드를 인식해 보세요.";
    }

    public void Duo_event()
    {
        State_text.text = PhotonNetwork.PlayerList[1].NickName + "님에게 \"결투\" 카드를 사용하세요.";
    }
    //B버튼에 넣을 이벤트
    public void Palyer2_Duo_Buttont()
    {
        photonView.RPC("Palyer2_Duo_RPC", RpcTarget.Others);
    }

    [PunRPC]
    private void Palyer2_Duo_RPC()
    {
        if (PhotonNetwork.PlayerList[1].NickName == PhotonNetwork.LocalPlayer.NickName)
            Duo_Panel.SetActive(true);
    }

    //Lose 버튼 이벤트
    public void Duo_Lose_button()
    {
        GetComponent<PlayerSet>().Minus_life(1);
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[4].NickName + "님이 "+ PhotonNetwork.PlayerList[1].NickName + "에게 \"결투\"를 신청하였습니다.");
        State_text.text = "이제 원체스터 카드를 장착해 보겠습니다.\n카드를 인식후 터치해 주세요.";
    }


    public void Winchester_Evet()
    {
        photonView.RPC("Player_Action_Please_All_RPC", RpcTarget.Others, PhotonNetwork.PlayerList[4].NickName + "님이 " +  "윈체스터 카드를 장착했습니다.\n 사거리가 5로 증가합니다.");
        check_Qution.SetActive(true);   //차례를 넘깁니다.
    }



}
