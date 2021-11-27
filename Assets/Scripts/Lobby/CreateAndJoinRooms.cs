using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    private RoomOptions roomOption;

    public Text roomNameText;
    public Button startButton;
    public string code;
    
    public InputField joinInput;
    void Start()
    {
        roomOption = new RoomOptions();
        roomOption.IsOpen = true;
        roomOption.MaxPlayers = 6;
        PhotonNetwork.NickName = PlayerManager.Instance.playerName;
        CreateRoom();
    }

    public void JoinRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom()
    {
        PlayerManager.Instance.InstantiatePlayer();
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.interactable = true;
        }
        else
        {
            startButton.interactable = false;
        }
        
    }
    
    public void CreateRoom()
    {
        code = RandomCode.Instance.NewRandomCode();
        PhotonNetwork.CreateRoom(code, roomOption);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Name room already taken. Trying to create another one...");
        CreateRoom();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Fail to join this room.");
        CreateRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfuly");
    }

    public override void OnLeftRoom()
    {
        Destroy(PlayerManager.Instance.myPlayer);
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection initiated successfully");
        PhotonNetwork.JoinLobby();
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player player)
    {
        Debug.Log("A player left the room");
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("You are now the master client");
            startButton.interactable = true;
        }
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsLocal)
            {
                PlayerManager.Instance.myPlayer.transform.position = SpawnManager.Instance.GetSpawnpoint(i).position;
                PlayerManager.Instance.myPlayer.transform.rotation = SpawnManager.Instance.GetSpawnpoint(i).rotation;
            }
        }
    }
}
