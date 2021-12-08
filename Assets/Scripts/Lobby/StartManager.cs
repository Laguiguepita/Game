using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartManager : MonoBehaviourPunCallbacks
{
    public Button startButton;

    public void StartGameSolo()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != 1)
        {
            Debug.Log("Can't");
            return;
        }
        else
        {
            PhotonNetwork.SetLevelPrefix(5);
            PhotonNetwork.LoadLevel(2);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public void StartGameMultiplayer()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1 || PhotonNetwork.CurrentRoom.PlayerCount > 3)
        {
            Debug.Log("Can't");
            return;
        }
        else
        {
            PhotonNetwork.SetLevelPrefix(5);
            PhotonNetwork.LoadLevel(2);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }

    public void StartGamePVP()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            Debug.Log("Can't");
            return;
        }
        else
        {
            PhotonNetwork.SetLevelPrefix(5);
            PhotonNetwork.LoadLevel(2);
            PhotonNetwork.CurrentRoom.IsOpen = false;
        }
    }
}
