using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AudioSo : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource AudioSource;
   [SerializeField] private AudioClip mainOst;
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = mainOst;
        AudioSource.volume=0.062F;
        AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = mainOst;
        AudioSource.volume=0.062F;

        if (!AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
       
    }
}
