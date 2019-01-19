using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class PhotonCommsManager : Photon.PunBehaviour
{

    private GameObject currentPlayer;

    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnCreatedRoom()
    {
        PhotonNetwork.Instantiate("Sphere", new Vector3(20.3f, 3.79f, 13.6f), Quaternion.identity, 0);
    }

    public override void OnJoinedRoom()
    {
        
        if (PhotonNetwork.countOfPlayersInRooms == 0)
        // instantiate user avatar locally and spawns in remote instances
            currentPlayer = PhotonNetwork.Instantiate("PlayerSpawnOculus", new Vector3(23.6f, 0.89f, 18.1f), new Quaternion(0f, 596.3f, 0f, 1f), 0);
        else
            currentPlayer = PhotonNetwork.Instantiate("PlayerSpawnOculus", new Vector3(-22.6f, 0.89f, -10.3f), new Quaternion(0f, 401.55f,0f,1f), 0);
    }

    // This is called if there is no one playing or if all rooms are full, so create a new room
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join room");
        PhotonNetwork.CreateRoom(null);
    }
}

