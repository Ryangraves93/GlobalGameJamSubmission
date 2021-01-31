using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    NavMeshAgent m_agent;
    MeshRenderer m_mesh;
    MeshFilter m_meshFilter;
    CapsuleCollider m_capsuleCollider;
    public Transform destination;

    void Start()
    {
        IntializeComponents();
    }

    private void Awake()
    {
        GameObject playerObj = FindObjectOfType<Player>().gameObject;
        destination = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAgent();   
    }

    void MoveAgent()
    {
        m_agent.SetDestination(destination.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.GameOver();
        }

        // This lets cops break through doors, as long as they believe they can walk through (make sure door's navmesh is walkable)
        if (collision.gameObject.CompareTag("breakable"))
        {
            Breakable breakableComponent = collision.gameObject.GetComponent<Breakable>();
            if (breakableComponent && breakableComponent.bPoliceCanBreak)
            {
                breakableComponent.breakMe();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            DestroyObject(other.gameObject);
        }
    }

    void DestroyObject(GameObject objectToBeDestroyed)
    {
        objectToBeDestroyed.gameObject.SetActive(false);
    }

    void IntializeComponents()
    {
        m_agent = GetComponent<NavMeshAgent>();
        m_mesh = GetComponent<MeshRenderer>();
        m_meshFilter = GetComponent<MeshFilter>();
        m_capsuleCollider = GetComponent<CapsuleCollider>();
    }
}
