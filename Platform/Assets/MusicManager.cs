using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  
    private AudioSource AudioSource;
   [SerializeField] private AudioClip arenaOst; 
   [SerializeField] private AudioClip mainOst; 
    public static MusicManager istance;
   private void Awake()
   {
       
       DontDestroyOnLoad(this);
        
       if (istance == null)
       {
           MusicManager.istance = this;
       }
       else
       {
           Destroy(gameObject);
       }

   }

   void Start()
   { 
       
      // arenaOst= (AudioClip)Resources.Load("AudioLevel1/arenaOst.mp3");
       //mainOst= (AudioClip)Resources.Load("AudioLevel1/mainOst.mp3");
       
       AudioSource = GetComponent<AudioSource>();
       PlayMainOst();
    }


   public void PlayArenaOst()
   {
       AudioSource.clip = arenaOst;
       AudioSource.Play();
   }
   
   public void PlayMainOst()
   {
       AudioSource.clip = mainOst;
       AudioSource.Play();
   }


   public void StopPlay()
   {
       AudioSource.Stop();
   }

    // Update is called once per frame
    void Update()
    {
        

    }
}
