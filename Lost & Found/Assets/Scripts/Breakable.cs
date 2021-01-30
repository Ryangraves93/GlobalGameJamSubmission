using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public AudioClip sound;
    public GameObject destroyedModel;
    public bool fragile = false;
    public void breakMe(){
        
        Debug.Log("break");
        Instantiate(destroyedModel, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
