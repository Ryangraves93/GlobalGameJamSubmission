using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioClip sound;
    public SoundBank.SoundType soundType;

    public GameObject destroyedModel;
    public bool fragile = false;
    public bool bPoliceCanBreak = false;
    bool broken = false;

    public void breakMe(){
        if (broken == false)
        {
            broken = true;
            GameManager.Instance.OnBreakObject(this);
            Instantiate(destroyedModel, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
