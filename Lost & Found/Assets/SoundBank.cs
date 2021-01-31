using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundBank : MonoBehaviour
{
    public AudioSource m_source;

    public AudioClip[] WoodClips;
    public AudioClip[] MetalClips;
    public AudioClip[] CeramicClips;

    public enum SoundType { Wood, Metal, Ceramic };

    // Start is called before the first frame update
    void Start()
    {
        m_source = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType soundType)
    {
        switch (soundType)
        {
        case SoundType.Wood:
             if (WoodClips.Length > 0)
             {
                 m_source.PlayOneShot(WoodClips[Random.Range(0, WoodClips.Length)], 1.0f);
             }
             break;

        case SoundType.Metal:
            if (MetalClips.Length > 0)
            {
                m_source.PlayOneShot(MetalClips[Random.Range(0, MetalClips.Length)], 1.0f);
            }
            break;
        case SoundType.Ceramic:
            if (CeramicClips.Length > 0)
            {
                m_source.PlayOneShot(CeramicClips[Random.Range(0, CeramicClips.Length)], 1.0f);
            }
            break;
        }

    }
        
     
    
}
