using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;  //씬 관련 메서드 관리 라이브러리

public class UpdateNum : MonoBehaviourPunCallbacks
{

    [Header("RoomTile")]
    public Text RoomName;   // 방 이름

    [Header("RoomInfo")]
    public Text Allnum;     //총 인원수
    public Text Nownum;     // 현재 인원수

    [Header("PlayerList")]
    public Text[] PlayerList;   //플레이어들이 뜰 텍스트 리스트

    void Awake()
    {
        RoomName.text = PhotonNetwork.CurrentRoom.Name; //방제목 넣어주고
        RoomRenewal();  //방 정보들 리뉴얼
    }

    //방 갱신 메서드
    void RoomRenewal()
    {
        //방에 있는 사람들의 
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PlayerList[i].text = PhotonNetwork.PlayerList[i].NickName;
        }

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
            RoomRenewal();
        }
    }

    //연결 끊기
    public void Disconnect()
    {
        PhotonNetwork.LeaveRoom(); //룸에서 나가고
        PhotonNetwork.Disconnect(); //연결도 끊기

        if (PhotonNetwork.IsMasterClient)
        {
            SceneManager.LoadScene("ARrecognize_pg");
        }
        else
        {
            SceneManager.LoadScene("Mainmenu");
        }

    }

    [PunRPC]//RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void AllRoomRenewalRPC()
    {
        //방에 있는 사람들의 리스트
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            PlayerList[i].text = PhotonNetwork.PlayerList[i].NickName;
        }

        Nownum.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString();    // 방에 입장해 있는 현재 인원 수
        Allnum.text = PhotonNetwork.CurrentRoom.MaxPlayers.ToString();     // 최대 인원수
    }

}
