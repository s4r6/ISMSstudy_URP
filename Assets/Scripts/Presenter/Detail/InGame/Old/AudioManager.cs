using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioClip[] seClips;
    AudioSource audioSource;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
    }

    

    public void playSE(int num)
    {
        audioSource.PlayOneShot(seClips[num]);

    }

   
}
