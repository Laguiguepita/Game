using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    
    public InputField nameInput; // ça part
    public string playerName; // ça part

    public GameObject playerPrefab; // ça part
    public GameObject myPlayer;
    private int numberOfPosition;

    public GameObject CreateAndJoinRooms;
    
    void Start()
    {
        Instance = this;
    }

    public void InstantiatePlayer()
    {
        numberOfPosition = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        myPlayer =
            (GameObject) PhotonNetwork.Instantiate(playerPrefab.name,
                SpawnManager.Instance.GetSpawnpoint(numberOfPosition).position,
                SpawnManager.Instance.GetSpawnpoint(numberOfPosition).rotation);
    }
    
    public void SetPlayerName()
    {
        playerName = nameInput.text;
        MenuManager.Instance.OpenMenu("LobbyMenu");
        CreateAndJoinRooms.SetActive(true);
    }
}
