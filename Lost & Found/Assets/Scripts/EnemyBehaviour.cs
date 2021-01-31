using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(AudioSource))]
public class EnemyBehaviour : MonoBehaviour
{
    public AudioClip m_sirenClip;
    public AudioClip m_doorBreakClip;

    // Start is called before the first frame update
    NavMeshAgent m_agent;
    MeshRenderer m_mesh;
    MeshFilter m_meshFilter;
    CapsuleCollider m_capsuleCollider;
    AudioSource m_audioSource;
    public Transform destination;

    void Start()
    {
        IntializeComponents();

        m_audioSource.clip = m_sirenClip;
        m_audioSource.loop = true;
        m_audioSource.Play();
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

                m_audioSource.PlayOneShot(m_doorBreakClip, 1.0f);
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
        m_audioSource = GetComponent<AudioSource>();
    }

}
