using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NetworkUI : MonoBehaviourPun
{
    //현재 이 스키립트를 실행하고 있는 User가 호스트(방장) 인가?
    public bool IsMasterClientLocal;
    public Button Startbutton;
    public Text Now;
    public float TimeCost = 10.0f;

    void Awake()
    {
        IsMasterClientLocal = PhotonNetwork.IsMasterClient;
    }

    public void Update()
    {
        //입장한 사람들의 수가 로비의 최대 인원수와 같다면
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if(TimeCost >= 0)
            {
                TimeCost -= Time.deltaTime;
                Now.text = TimeCost.ToString("N1") + "초 뒤 자동으로 \n 연습게임으로 이동합니다.";
                //10초뒤에 씬 이동
                StartCoroutine(Count());
            }
            
        }
        if (IsMasterClientLocal)    //호스트면
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)    // 현재 인원수와 모든 인원수가 같다면.
                Startbutton.interactable = true;    //버튼 활성화
            else
                Startbutton.interactable = false;   //버튼 비활성화
        }
        else    //호스트가 아니면
        {
            Startbutton.interactable = false;   //버튼 비활성화
        }
    }


    public void GameStart()
    {
        //플레이어 전체에게 RPC 실행
        photonView.RPC("GameStartRPC", RpcTarget.All);
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(10.0f);
        //PhotonNetwork.LoadLevel("Bang_pg");
        PhotonNetwork.LoadLevel("New Scene");
        TimeCost = 10.0f;
    }


    [PunRPC]//RPC는 플레이어가 속해있는 방 모든 인원에게 전달한다
    void GameStartRPC()
    {
        PhotonNetwork.LoadLevel("Bang_pg");    //동기화가 자동으로됨.
    }

}
