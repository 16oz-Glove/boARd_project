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
    public Text Countdown;
    public float delayTime = 10f;

    void Awake()
    {
        IsMasterClientLocal = PhotonNetwork.IsMasterClient;
    }

    void Update()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)    // 현재 인원수와 모든 인원수가 같다면.
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Startbutton.interactable = true;   //버튼 비활성화
            }

            if (delayTime >= 0)
            {
                delayTime -= Time.deltaTime;
                NotiseTimeRPC(delayTime);  //모든 방에 시간 알려주기.
                StartCoroutine(CountReady());   //코루틴 함수 불러온다
            }
            else
            { 
                Startbutton.interactable = false;   //버튼 비활성화
            }
        }
    }

    //코루틴 함수
    IEnumerator CountReady()
    {
        yield return new WaitForSeconds(delayTime); //10초 기다린다.
        Startbutton.interactable = true;   //버튼 활성화
        GameStart(); // 게임 시작
    }

    void GameStart()
    {
        PhotonNetwork.LoadLevel("Bang_pg");    //동기화가 자동으로됨.
    }

    void NotiseTimeRPC(float delayTime)
    {
        Countdown.text = delayTime.ToString("N1") + " 초 뒤,\n 자동으로 이동합니다.";
    }
}
