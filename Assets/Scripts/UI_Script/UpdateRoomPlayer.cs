using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class UpdateRoomPlayer : MonoBehaviourPunCallbacks
{
    [Header("RoomTile")]
    public Text RoomName;   // 방 이름

    [Header("RoomInfo")]
    public Text Allnum;     //총 인원수
    public Text Nownum;     // 현재 인원수

    [Header("스타트 버튼")]
    private Button Startbutton;
    private GameObject Startbutton2;

    [Header("게임시작캔버스")]
    public GameObject panel;

    [Header("가이드캔버스")]
    public GameObject guide_panel;

    [Header("사용자 상태/명력 텍스트")]
    public GameObject State_text;    // 미니맵 중앙에 알림 표시할 부분

    void Awake()
    {
        RoomName.text = PhotonNetwork.CurrentRoom.Name; //방제목 넣어주고
        RoomRenewal();  //방 정보들 리뉴얼

        //버튼 찾아서 True 시켜주고
        Startbutton2 = GameObject.Find("Canvas").transform.Find("Panel").transform.Find("WaitPanel").transform.Find("Button").gameObject;
        Startbutton2.SetActive(true);
        //버튼 컴포넌트를 등록해준다음, 아직 사용못하게 하기
        Startbutton = Startbutton2.GetComponent<Button>();
        Startbutton.interactable = false;
    }

    public void FixedUpdate()
    {
        //입장한 사람들의 수가 로비의 최대 인원수와 같다면
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)    //호스트면
              Startbutton.interactable = true;
        }
    }

    //방 갱신 메서드
    void RoomRenewal()
    {
        Nownum.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();    // 방에 입장해 있는 현재 인원 수
        Allnum.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();     // 최대 인원수
    }

    //방에 입장했을때
    public override void OnJoinedRoom()
    {
        RoomRenewal();
    }

    public override void OnPlayerEnteredRoom(Player other)
    {
        RoomRenewal();

    }

    //사람이 나갔을때
    public override void OnLeftRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 0)
        {
            return;
        }
        else
        {
            // 방 나가면서 다른 플레이어 화면 업뎃
            photonView.RPC("RenewalRPC",RpcTarget.All);
        }
    }

    //연결 끊기
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom(); //룸에서 나가고
        PhotonNetwork.Disconnect(); //연결도 끊기

        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("ARrecognize");
        }
        else
        {
            SceneManager.LoadScene("Mainmenu");
        }
    }

    public void Button_GameStart()
    {
        photonView.RPC("GameStartRPC", RpcTarget.All);
        UIManager.instance.Player1_Turn1_Text();    //사용자1 화면 업데이트 해주기
    }

    [PunRPC]
    public void GameStartRPC()
    {
        panel.SetActive(false);
        guide_panel.SetActive(true);
        State_text.SetActive(true);
        UIManager.instance.set_notice_Turn(1);   //보안관 차례임을 미니맵에 표시

        // 각 사용자에게게 현 보드게임 이름 등록하고 DB에 로그 저장
        BoardName.Name_Scene = PhotonNetwork.CurrentRoom.CustomProperties["BoardName"].ToString();
        LogRecord.AddLog_pg();
    }

    [PunRPC]
    public void RenewalRPC()
    {
        Nownum.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();    // 방에 입장해 있는 현재 인원 수
        Allnum.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();     // 최대 인원수
    }

}
