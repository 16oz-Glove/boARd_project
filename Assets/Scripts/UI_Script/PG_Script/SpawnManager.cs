using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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
    public GameObject minimap;
    public GameObject CanvusMap_parent;

    private void Start()
    {
        Spawn();
        /*
        //미니맵 생성
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnMap();
        }
        */
    }

    //사용자들 스폰위치에 스폰 시키기
    private void Spawn()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber-1;
        var spawnPlace = spawnPlaces[localPlayerIndex % spawnPlaces.Length];

        //오브젝트 생성해서
        PhotonNetwork.Instantiate(playerPref.name, spawnPlace.position, spawnPlace.rotation).transform.parent = CanvusMap_parent.transform;
    }
    /*
    private void SpawnMap()
    {
        PhotonNetwork.Instantiate(minimap.name, );
    }
    */
}
