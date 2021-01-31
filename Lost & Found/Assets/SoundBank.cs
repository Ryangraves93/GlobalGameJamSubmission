using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBank : MonoBehaviour
{
    public AudioSource source;

    public AudioClip[] WoodClips;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlaySound(int Index)
    {
        switch (Index)
        {
        case 0:
             if (WoodClips.Length > 0)
             {
                 source.clip = WoodClips[Random.Range(0, WoodClips.Length)];
             }
             break;
            
        }

        source.Play();
    }
        
     
    
}
