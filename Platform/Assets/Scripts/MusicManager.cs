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
   [SerializeField] private AudioClip menuOst;
    public static MusicManager istance;
    public static AudioClip playedClip;
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
       PlayMenuOst();
    }


   public void PlayArenaOst()
   {
       AudioSource.clip = arenaOst;
       playedClip = arenaOst;
       AudioSource.Play();
   }
   
   public void PlayMainOst()
   {
       AudioSource.clip = mainOst;
       playedClip = mainOst;
       AudioSource.Play();
   }
   public void PlayMenuOst()
   {
       AudioSource.clip = menuOst;
       playedClip = mainOst;
       AudioSource.Play();
   }


   public void StopPlay()
   {
       playedClip = null;
       AudioSource.Stop();
   }

   public bool IsPlaying()
   {
       return playedClip != null;
       
   }

   public bool IsPlayingArenaOst()
   {
       return playedClip.Equals(arenaOst);
   }

    // Update is called once per frame
    void Update()
    {
        

    }
}
