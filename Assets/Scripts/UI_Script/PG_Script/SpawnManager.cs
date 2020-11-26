using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPlaces;
    public GameObject playerPref;
    public GameObject CanvusMap;

    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //사용자들 스폰위치에 스폰 시키기
    public void Spawn()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber;
        var spawnPlace = spawnPlaces[localPlayerIndex % spawnPlaces.Length];

        //minimap의 자식으로 들어가서 미니맵에 띄어지도록 하기
        CanvusMap.transform.parent = PhotonNetwork.Instantiate(playerPref.name, spawnPlace.position, spawnPlace.rotation).transform;
    }
}
