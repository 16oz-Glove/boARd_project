using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;  //MonoBehaviourPunCallbacks 상속을 위한 using

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public static SpawnManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<SpawnManager>();

            return instance;
        }
    }

    private static SpawnManager instance;

    public Transform[] spawnPlaces;
    public GameObject playerPref;
    public GameObject CanvusMap_parent;

    private void Start()
    {
        Spawn();
    }


    //사용자들 스폰위치에 스폰 시키기
    private void Spawn()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber-1; // 0부터 시작
        var spawnPlace = spawnPlaces[localPlayerIndex % spawnPlaces.Length];

        //생성되는거 까지는 동기화가 되지만, 부모에 가져다 붙이는 과정에서 동기화가 안됨.
        //이 부분을 RPC로 해결하면 될것 같음.(자기는 minimap에 붙어 있지만, 다른 플레이어는 그렇지 않음)
        //다른 사용자의 prefap 오브젝트를 찾아서 미니맵 밑으로 넣어주기.(가능한가?)
        //PhotonNetwork.Instantiate(playerPref.name, spawnPlace.position, spawnPlace.rotation).transform.parent = CanvusMap_parent.transform;

        //2번 대안 사용! : 그냥 3d좌표 아무곳이나 사용자 나타내는 오브젝트 생성.
        PhotonNetwork.Instantiate(playerPref.name, spawnPlace.position, spawnPlace.rotation);
    }

    //각 플레이어들의 버튼 프리팹 오브젝트가 미니맵 안에 위치하도록 동기화 시키기!
    [PunRPC]
    private void positioningRPC(Transform spawnPlace)
    {
        //오브젝트 생성해서
        PhotonNetwork.Instantiate(playerPref.name, spawnPlace.position, spawnPlace.rotation).transform.parent = CanvusMap_parent.transform;
    }

}
