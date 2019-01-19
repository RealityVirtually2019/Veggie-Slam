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

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.countOfPlayersInRooms == 0)
        // instantiate user avatar locally and spawns in remote instances
            currentPlayer = PhotonNetwork.Instantiate("PlayerSpawn", new Vector3(0, 1.235f, 2.65f), Quaternion.identity, 0);
        else
            currentPlayer = PhotonNetwork.Instantiate("PlayerSpawn", new Vector3(0, 1.235f, 0f), Quaternion.identity, 0);
    }

    // This is called if there is no one playing or if all rooms are full, so create a new room
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join room");
        PhotonNetwork.CreateRoom(null);
    }
}

