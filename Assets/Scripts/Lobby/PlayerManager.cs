using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    
    public InputField nameInput; // ça part
    public string playerName; // ça part

    public GameObject playerPrefab; // ça part
    public GameObject myPlayer;
    private int numberOfPosition;

    public GameObject CreateAndJoinRooms;

    private Scene currentScene;
    
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        Instance = this;
        if (currentScene.name == "Game")
        {
            Vector2 randomPosition = new Vector3(5,1,5);
            myPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
            myPlayer.transform.Find("GameObject").gameObject.SetActive(true);
            myPlayer.GetComponent<mouvement>().enabled = true;
        }
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
