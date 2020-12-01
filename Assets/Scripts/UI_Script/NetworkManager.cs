using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;       //MonoBehaviourPunCallbacks 상속을 위한 using
using Photon.Realtime;  //MonoBehaviourPunCallbacks 상속을 위한 using
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";
    public Text StatusText;         //네트워크 접속상태를 나타내는 
    public InputField roomInput, NickNameInput;
    public Button joinButton;           //룸에 접속할때 누르는 Button 변수
    static string Boardgame;   //로비로 들고갈 변수
    static byte MaxplayerNum;      //방장이 선택할 방 인원 수, 부호 없는 정수를 나타내는 변경할 수 없는 값 형식
    bool Disconnect_choose;         //사용자가 연결을 직접 끊었냐 안끊었냐
    public GameObject error;

    public Button playerNum2_bt;           //사람인원수2 버튼
    public Button playerNum3_bt;           //사람인원수3 버튼
    public Button playerNum4_bt;           //사람인원수4 버튼
    public Button playerNum5_bt;           //사람인원수5 버튼
    public Button playerNum6_bt;           //사람인원수6 버튼
    public Button playerNum7_bt;           //사람인원수7 버튼
    public Button playerNum8_bt;           //사람인원수8 버튼
    

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        StatusText.text = "마스터 서버 접속 중...";
        PhotonNetwork.ConnectUsingSettings();        // 마스터 서버에 접속을 성공하면, 밑에 OnConnectedToMaster() 메서드가 자동으로 실행됨.
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //'방 만들기 버튼을 눌렀을때 실행될 메서드'
    public void Connet()
    {
        joinButton.interactable = false;    //마스터 서버에 접속이 완료될때까지 잠시 버튼 비활성화(중복 접속 방지)

        //안정장치. 버튼을 누르는 순간 네트워크가 끊길수도 있으므로,
        if (PhotonNetwork.IsConnected)  //잘 접속이 되었다면
        {
            PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;    //유저 닉네임 입력한 부분을 등록
            joinButton.interactable = false;    //서버에 접속이 성공하면 비활성화.
            StatusText.text = "방을 만드는 중입니다...";

            // 방장이 입력한 방 제목과, 최대 인원수를 가진 방을 생성.
            RoomOptions roomOption = new RoomOptions();
            roomOption.MaxPlayers = MaxplayerNum;
            roomOption.CustomRoomProperties = new Hashtable() { { "BoardName", BoardName.Name_Scene } };
            PhotonNetwork.CreateRoom(roomInput.text, roomOption);
        }
        else        //잘 접속이 되지 않았다면
        {
            joinButton.interactable = true; //마스터 서버에 접속을 성공했으므로, 룸으로 들어가는 버튼 활성화
            StatusText.text = "서버와의 연결이 끊겼습니다. 재 접속 시도중...";  //실패한 사유를 텍스트로 출력
            PhotonNetwork.ConnectUsingSettings();   //접속을 실패했으니 재 접속 시도.
        }
    }

    //'연습게임방 입장' 버튼을 눌렀을때 실행될 메서드
    public void RemoteConnet()
    {
        joinButton.interactable = false;    //마스터 서버에 접속이 완료될때까지 잠시 버튼 비활성화(중복 접속 방지)

        //안정장치. 버튼을 누르는 순간 네트워크가 끊길수도 있으므로,
        if (PhotonNetwork.IsConnected)  //잘 접속이 되었다면
        {
            PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;    //유저 닉네임 입력한 부분을 등록
            joinButton.interactable = false;    //서버에 접속이 성공하면 비활성화.
            StatusText.text = "입력하신 정보와 일치하는 방을 찾는 중입니다...";
            // 리모트가 입력한 방 제목을 찾아 방에 입장.
            PhotonNetwork.JoinRoom(roomInput.text);
        }
        else        //잘 접속이 되지 않았다면
        {
            joinButton.interactable = true; //마스터 서버에 접속을 성공했으므로, 룸으로 들어가는 버튼 활성화
            StatusText.text = "서버와의 연결이 끊겼습니다. 재 접속 시도중...";  //실패한 사유를 텍스트로 출력
            PhotonNetwork.ConnectUsingSettings();   //접속을 실패했으니 재 접속 시도.
        }
    }

    //Connet() 함수의 CallBack 함수 (CallBack 함수는 주로 'On' 으로 시작한다.)
    public override void OnConnectedToMaster()
    {
        StatusText.text = "서버접속완료. 방에 대한 정보(제목)을 입력하세요.";
    }

    //Master server에 접속을 실패했거나, 이미 접속했는데 네트워크가 끊어진 경우에 실행되는 메서드
    public override void OnDisconnected(DisconnectCause cause)  //접속하지 못한 이유(cause) 변수가 들어가게됨.
    {
        if (Disconnect_choose) //사용자가 연결을 끊은 경우
        {
            print("연결이 끊어졌습니다.");
            Disconnect_choose = false;  // Disconnect_choose변수 다시 클리어.
            return;
        }
        else  //네트워크 상태가 안좋아서 연결이 그냥 끊긴경우
        {
            joinButton.interactable = false;   //마스터 서버에 접속이 실패했으므로 잠시 버튼 비활성화
            StatusText.text = $"Offline : Connection Disabled {cause.ToString()} - Try Reconnecting";  //실패한 사유를 텍스트로 출력

            PhotonNetwork.ConnectUsingSettings();   //접속을 실패했으니 재 접속 시도.
        }
    }

    //연결 끊기
    public void Disconnect()
    {
        Disconnect_choose = true;
        PhotonNetwork.Disconnect(); //연결도 끊기
    }

    //방 생성(PhotonNetwork.CreateRoom())에 성공한 경우, 콜백
    public override void OnCreatedRoom()
    {
        StatusText.text = "방생성에 성공하였습니다.";
    }

    //방 생성(PhotonNetwork.CreateRoom())에 성공한 경우, 콜백
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        StatusText.text = "방생성에 실패하였습니다.";
        //다시 방 생성 시도
        PhotonNetwork.CreateRoom(roomInput.text, new RoomOptions { MaxPlayers = MaxplayerNum }, null);
    }

    //방 생성(PhotonNetwork.CreateRoom())에 성공한 경우, 콜백
    public override void OnJoinedRoom()
    {
        StatusText.text = "방에 입장합니다.";
        //SceneManager.LoadScene(); 으로 이동하면 안됨. 동기화가 전혀 안됨. 독자적으로 자기 세계에서만 씬을 이동함.
        PhotonNetwork.LoadLevel("New Scene");    //동기화가 자동으로됨.
    }


    //방 입장에 실패한 경우
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        joinButton.interactable = true;    //실패 하였으므로 다시 시도
        StatusText.text = "방 입장에 실패하였습니다. 다시 시도해주세요.";
        error.SetActive(true);
}

    public void SelectTheNum2()
    {
        print("눌렸음");
        print("2명 입니다");
        InteratableButton();
        playerNum2_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 2;
    }

    public void SelectTheNum3()
    {
        print("눌렸음");
        InteratableButton();
        playerNum3_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 3;
    }

    public void SelectTheNum4()
    {
        print("눌렸음");
        InteratableButton();
        playerNum4_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 4;
    }
    public void SelectTheNum5()
    {
        print("눌렸음");
        InteratableButton();
        playerNum5_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 5;
    }
    public void SelectTheNum6()
    {
        print("눌렸음");
        InteratableButton();
        playerNum6_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 6;
    }
    public void SelectTheNum7()
    {
        print("눌렸음");
        InteratableButton();
        playerNum7_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 7;
    }
    public void SelectTheNum8()
    {
        print("눌렸음");
        InteratableButton();
        playerNum8_bt.interactable = false; //누른 버튼 비활성화
        MaxplayerNum = 8;
    }

    //버튼 활성화
    public void InteratableButton()
    {
        playerNum2_bt.interactable = true; //누른 버튼 활성화
        playerNum3_bt.interactable = true; //누른 버튼 활성화
        playerNum4_bt.interactable = true; //누른 버튼 활성화
        playerNum5_bt.interactable = true; //누른 버튼 활성화
        playerNum6_bt.interactable = true; //누른 버튼 활성화
        playerNum7_bt.interactable = true; //누른 버튼 활성화
        playerNum8_bt.interactable = true; //누른 버튼 활성화
    }
}
