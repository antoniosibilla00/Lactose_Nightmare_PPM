using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    
    [SerializeField] private AudioMixer audioMixer;
    public static AudioManager instance;

    private void Awake(){
        if(instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
            
          
        }
        else
            Destroy(gameObject);
    }

    public void Start(){
        
        float music = PlayerPrefs.GetFloat("MusicVolume",0f);
        float effects = PlayerPrefs.GetFloat("EffectVolume", 0f);
        float general = PlayerPrefs.GetFloat("GeneralVolume", 0f);
        
        AdjustMusicVolume(music);
        AdjustEffectVolume(effects);
        AdjustGeneralVolume(general);
      
        MusicManager.istance.PlayMenuOst();
        
    }
    
    public void AdjustMusicVolume(float volume){
        //Update AudioMixer
        
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume",volume);

        //Save changesS
        PlayerPrefs.Save();
    }

    public void AdjustEffectVolume(float volume){
        //Update AudioMixer
   
        audioMixer.SetFloat("EffectVolume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("EffectVolume",volume);

        //Save changes
        PlayerPrefs.Save();
    }

    public void AdjustGeneralVolume(float volume){
        //Update AudioMixer
   
        audioMixer.SetFloat("GeneralVolume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("GeneralVolume",volume);

        //Save changes
        PlayerPrefs.Save();
    }
    
    
   

 

}
 

