using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider generalSlider;


    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(AudioManager.instance.AdjustMusicVolume);
        effectSlider.onValueChanged.AddListener(AudioManager.instance.AdjustEffectVolume);
        generalSlider.onValueChanged.AddListener(AudioManager.instance.AdjustGeneralVolume);
    }

    private void Start()
    {

        float music = PlayerPrefs.GetFloat("MusicVolume",0f);
        float effects = PlayerPrefs.GetFloat("EffectVolume", 0f);
        float general = PlayerPrefs.GetFloat("GeneralVolume", 0f);
        
        musicSlider.value = music;
        effectSlider.value = effects;
        generalSlider.value = general;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
