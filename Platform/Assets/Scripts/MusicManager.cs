using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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
       AudioSource = GetComponent<AudioSource>();

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

   public bool IsPlayingMenuOst()
   {
       Debug.Log("playedClip "+playedClip.name);
       return playedClip.name.Equals(menuOst.name);
   }

    // Update is called once per frame


}
