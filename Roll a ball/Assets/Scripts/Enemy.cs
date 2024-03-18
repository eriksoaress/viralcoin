using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent inimigo;
    private Transform player;



    void Start()
    {
        inimigo = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;

    }

    void Update()
    {
        inimigo.SetDestination(player.position);
        
    }

    
}
