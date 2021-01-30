using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent agent;
    public Transform destination;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveAgent();   
    }

    void MoveAgent()
    {
        agent.SetDestination(destination.position);
    }
}
