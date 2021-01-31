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
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.collider);

        if (col.collider.tag == "breakable")
        {
            Debug.Log("Player is trying to break!");
        }
    }

}
