using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkUI : MonoBehaviourPunCallbacks
{
    //현재 이 스키립트를 실행하고 있는 User가 호스트(방장) 인가?
    public bool IsMasterClientLocal;
    public Button Startbutton;
    public Text Now;
    public float TimeCost = 10.0f;

    void Awake()
    {
        IsMasterClientLocal = PhotonNetwork.IsMasterClient;
        // 플레이어 수에 따라 변경되는 경기장 세팅에 사용. true면 Master클라이언트에서 LoadLevel()을 호출할 수 있다.
        // 이때 방의 모든 클라이언트가 마스터 클라이언트와 동일한 레벨을 자동으로 로드함. 즉 true시 레벨 동기화.
        PhotonNetwork.AutomaticallySyncScene = true;
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
                if (IsMasterClientLocal)    //호스트면
                    StartCoroutine(Count());    // 10초뒤 게임시작
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

    IEnumerator Count()
    {
        yield return new WaitForSeconds(10.0f);
        //PhotonNetwork.LoadLevel("Bang_pg");
        PhotonNetwork.LoadLevel("New Scene");
        TimeCost = 10.0f;
    }

    private void GetCurrentRoomPlayers()
    {

    }

}
