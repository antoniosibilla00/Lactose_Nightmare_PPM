using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider generalSlider;
    public static AudioManager instance;

    private void Awake(){
        if(instance == null){
            DontDestroyOnLoad(gameObject);
            instance = this;
            
            musicSlider.onValueChanged.AddListener(AdjustMusicVolume);
            effectSlider.onValueChanged.AddListener(AdjustEffectVolume);
            generalSlider.onValueChanged.AddListener(AdjustGeneralVolume);
          
        }
        else
            Destroy(gameObject);
    }

    public void Start(){
        float music = PlayerPrefs.GetFloat("MusicVolume",0f);
        float effects = PlayerPrefs.GetFloat("EffectVolume", 0f);
        float general = PlayerPrefs.GetFloat("GeneralVolume", 0f);

        musicSlider.value = music;
        effectSlider.value = effects;
        generalSlider.value = general;
        
        MusicManager.istance.PlayMenuOst();



        //Set the music volume to the saved volume

    }
    
    public void AdjustMusicVolume(float volume){
        //Update AudioMixer
        
        audioMixer.SetFloat("MusicVolume",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume",volume);

        //Save changes
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
 

