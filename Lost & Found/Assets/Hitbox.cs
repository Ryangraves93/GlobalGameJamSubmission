using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    SphereCollider m_SphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_SphereCollider = GetComponent<SphereCollider>();
        m_SphereCollider.enabled = false;
    }

    public void Trigger()
    {
        m_SphereCollider.enabled = true;
        StartCoroutine(StopTrigger());
    }

    IEnumerator StopTrigger()
    {
        yield return new WaitForSeconds(0.1f);
        m_SphereCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider);

        if (col.collider.tag == "breakable")
        {
            Debug.Log("Player is trying to break!");
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col);

        if (col.tag == "breakable")
        {
            col.GetComponent<Breakable>().breakMe();

            Debug.Log("Player is trying to break! - TRIGGERD");
        }
    }

}
