using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;
    void Start()
    {
        Vector2 randomPosition = new Vector2(5,5);
        GameObject myPlayer =
            (GameObject) PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        myPlayer.transform.Find("GameObject").gameObject.SetActive(true);
        myPlayer.GetComponent<mouvement>().enabled = true;
    }

    
}
