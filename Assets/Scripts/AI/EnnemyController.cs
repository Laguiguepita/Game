using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        //target = PlayerManager.Instance.myPlayer.transform;
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
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
