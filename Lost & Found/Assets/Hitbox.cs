using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    SphereCollider m_SphereCollider;
    SphereCollider m_debrisPusherCollider;


    public List<Collider> m_Colliders;

    //public bool bAttackActive;

    // Start is called before the first frame update
    void Start()
    {
        m_Colliders = new List<Collider>();

        m_SphereCollider = GetComponent<SphereCollider>();
        m_debrisPusherCollider = GetComponentInChildren<SphereCollider>();

        m_SphereCollider.enabled = true;
        m_debrisPusherCollider.enabled = true;
        //bAttackActive = false;
    }

    public void Trigger()
    {
        foreach (Collider col in m_Colliders)
        {
            Breakable breakable = col.GetComponent<Breakable>();
            if (breakable)
            {
                breakable.breakMe();
            }
        }
        m_Colliders.Clear();

        //Debug.Log("Triggered");



        //m_SphereCollider.enabled = true;
        //m_debrisPusherCollider.enabled = true;
        //bAttackActive = true;


        //StartCoroutine(StopTrigger());
    }

    IEnumerator StopTrigger()
    {
          yield return new WaitForSeconds(0.01f);
    //    m_SphereCollider.enabled = false;
          m_debrisPusherCollider.enabled = false;
    //    bAttackActive = false;
    //
    //    //yield return new WaitForSeconds(0.1f);
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

        if (col.tag == "breakable")
        {
            m_Colliders.Add(col);

            //col.GetComponent<Breakable>().breakMe();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        m_Colliders.Remove(col);
    }

}
