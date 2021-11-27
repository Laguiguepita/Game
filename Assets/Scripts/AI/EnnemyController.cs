using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    
    GameObject[] targets;
    NavMeshAgent agent;
    private int closestPlayer;
    private float closestPlayerDistance;
    
        
    void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        closestPlayerDistance = lookRadius;
        for (int i = 0; i < targets.Length; i++)
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            float distance = Vector3.Distance(targets[i].transform.position, transform.position);
            if (distance <= closestPlayerDistance)
            {
                closestPlayerDistance = distance;
                closestPlayer = i;
            }
        }
        if (closestPlayerDistance < lookRadius)
        {
            agent.SetDestination(targets[closestPlayer].transform.position);
            if (closestPlayerDistance <= agent.stoppingDistance)
            {
                //attack player
                RPC_Damage();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    [PunRPC]
    void RPC_Damage()
    {
        targets[closestPlayer].GetComponent<Player>().TakeDamage(10);
    }
}
