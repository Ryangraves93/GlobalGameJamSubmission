using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioClip sound;
    public GameObject destroyedModel;
    public bool fragile = false;
    public bool bPoliceCanBreak = false;
    bool broken = false;

    public void breakMe(){
        if (broken == false)
        {
            broken = true;
            Debug.Log("break");
            GameManager.Instance.OnBreakObject(this);
            Instantiate(destroyedModel, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
