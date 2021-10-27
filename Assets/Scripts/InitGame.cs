using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InitGame : MonoBehaviourPunCallbacks
{
    public GameObject ennemy;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.InstantiateRoomObject(ennemy.name, new Vector3(25, 2,10), new Quaternion());
        }
    }
}
