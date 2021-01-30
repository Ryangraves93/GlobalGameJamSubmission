using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent agent;
    MeshRenderer mesh;
    MeshFilter meshFilter;
    public Transform destination;

    void Start()
    {
        IntializeComponents();
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

    void IntializeComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();
    }
}
