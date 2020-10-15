using Photon.Pun;                  //포톤 서비스를 이용하기위한 using import
using Photon.Realtime;             //포톤 서비스를 이용하기위한 using import
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks   //일반적인 MonoBehaviour 스크립트랑은 다름. PUN2 전용
{
    //게임 버전에 따라서 멀티매칭이 가능할 수도 있고 아닐수도 있다.
    //게임 버전이 다르면 같은게임이지만 매치매이킹이 안되는게 정상.
    private readonly string gameVersion = "1";

    public Text connectionInfoText;     //네트워크 상태를 출력할 Text 변수
    public Button joinButton;           //룸에 접속할때 누르는 Button 변수

    //로비에 진입함과 동시에 Master서버에 접속을 시도! (Mater server : 포톤 클라우드 서버이자 매치메이킹을 위한 server)
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;    //포톤 네트워크의 게임버전에 우리 네트워크 게임버전 명시
        PhotonNetwork.ConnectUsingSettings();       //여러 부가적인 설정 정보를 가지고 접속 시도.(마스터서버에 접속 시도하는 메서드)
        // 마스터 서버에 접속을 성공하면, 밑에 OnConnectedToMaster() 메서드가 자동으로 실행됨.

        joinButton.interactable = false;    //마스터 서버에 접속이 완료될때까지 잠시 버튼 비활성화
        connectionInfoText.text = "Connecting To Master Server..."; //마스터 서버에 접속중임을 표시



    }

    //접속상태에 따라서 원하는 부분을 실행해야하기 때문에 MonoBehaviourPunCallbacks를 상속하였다.
    //Master서버에 접속이 된 순간에 무엇가를 실행하고 싶다면 MonoBehaviourPunCallbacks를 상속하여
    //우리가 원하는 처리가 실행된다.
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true; //마스터 서버에 접속을 성공했으므로, 룸으로 들어가는 버튼 활성화
        connectionInfoText.text = "Online : Connected To Master Server"; //마스터 서버에 접속이 성공했음을 출력

    }

    //Master server에 접속을 실패했거나, 이미 접속했는데 네트워크가 끊어진 경우에 실행되는 메서드
    public override void OnDisconnected(DisconnectCause cause)  //접속하지 못한 이유(cause) 변수가 들어가게됨.
    {
        joinButton.interactable = false;   //마스터 서버에 접속이 실패했으므로 잠시 버튼 비활성화
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()} - Try Reconnecting";  //실패한 사유를 텍스트로 출력

        PhotonNetwork.ConnectUsingSettings();   //접속을 실패했으니 재 접속 시도.
    }

    //joinButton을 눌렀을때 실행될 메서드. *Photon 메서드가 아님.
    public void Connect()
    {
        joinButton.interactable = false;

        //안정장치. 버튼을 누르는 순간 네트워크가 끊길수도 있으므로,
        if (PhotonNetwork.IsConnected)  //잘 접속이 되었다면
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom(); // 빈방이 있다면 랜덤으로 배정. 만약 빈방에 없다면, 밑에 OnJoinRandomFailed 메서드 실행.
        }
        else        //잘 접속이 되지 않았다면
        {
            connectionInfoText.text = "Offline : Connection Disabled - Try Reconnecting";  //실패한 사유를 텍스트로 출력

            PhotonNetwork.ConnectUsingSettings();   //접속을 실패했으니 재 접속 시도.
        }
    }

    //빈방이 없는경우 실행되는 메서드.
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room, Creating new Room.";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });

        /*PhotonNetwork.CreateRoom() 메서드에 관해서.
         * https://doc-api.photonengine.com/ko-kr/pun/current/class_photon_network.html
         * 위 링크참고하기
          */

    }

    //방 접속에 성공했거나, 실패했다가 새로 방을 형성해서 다시 접속이 성공한 경우
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connecting with Room";
        //SceneManager.LoadScene(); 으로 이동하면 안됨. 동기화가 전혀 안됨. 독자적으로 자기 세계에서만 씬을 이동함.
        PhotonNetwork.LoadLevel("Main");    //동기화가 자동으로됨.
    }
}