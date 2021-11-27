using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviourPunCallbacks, IPunObservable
{
    private  const float maxHealth = 3000f;
    private float currentHealth;

    private Text healthBar;

    public Slider slider;
    void Start()
    {
        //healthBar = FindObjectOfType<Text>();
        SetDefaults();
    }
    
    void Update()
    {
        
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        //healthBar.text = currentHealth.ToString();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        RPC_HealthDisplay();
        //healthBar.text = currentHealth.ToString();
        Debug.Log(transform.name + " a maintenant : " + currentHealth + " points de vies.");
    }

    [PunRPC]
    void RPC_HealthDisplay()
    {
        slider.value = currentHealth;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(currentHealth);
        }
        else if (stream.IsReading)
        {
            currentHealth = (float) stream.ReceiveNext();
        }
    }
}
