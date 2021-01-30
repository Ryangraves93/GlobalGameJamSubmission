using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckFragmentsCoroutine());
    }

    IEnumerator CheckFragmentsCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        if (CheckFragments())
        {
            StartCoroutine(CheckFragmentsCoroutine());
        }

    }


    bool CheckFragments()
    {
        bool bFragmentsRemaining = false;

        foreach (Transform child in transform)
        {
            Rigidbody rb = child.gameObject.GetComponent<Rigidbody>();
            
            
            if (rb && rb.isKinematic == false)
            {
                if (rb.velocity.sqrMagnitude < 0.01 )
                {
                    rb.isKinematic = true;
                    //child.gameObject.GetComponent<>

                    Debug.Log("Setting isKinematic to false");
                }
                else
                {
                    // if any fragment still has velocity...
                    bFragmentsRemaining = true;
                }
            }
        }
        return bFragmentsRemaining;
    }
}
