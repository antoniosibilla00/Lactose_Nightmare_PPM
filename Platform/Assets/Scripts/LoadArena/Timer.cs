using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Timer : MonoBehaviour
{
    string[] insights = {"Il 90% dei nord Europei mantiene l’abilità di digerire il lattosio dopo l’infanzia contro il 5% degli Asiatici",
        "Alcuni soggetti intolleranti accusano irritabilità e sbalzi d’umore", 
        "Il valore soglia dell’intolleranza varia da persona a persona. In alcuni casi i sintomi sono talmente lievi da essere ignorati", 
        "La probabilità di essere intolleranti sale per quella parte della popolazione la cui dieta normalmente esclude i latticini"};
    [SerializeField] public Text insightsText1;
    public Slider timerSlider;
    public float gameTime;
    private float timer;
    [SerializeField] public Canvas loadArena;
    [SerializeField] private GameObject destination;
    [SerializeField] public GameObject ground;
    [SerializeField] public GameObject player;
    private bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        stopTimer = false;
        timerSlider.maxValue = gameTime;
        timerSlider.value = gameTime;
        player.SetActive(false);
        ground.SetActive(false);
        
        insightsText1.text =  insights[Random.Range(0 , 4)]; 
        
    }

    // Update is called once per frame
  

    private void Update()
    {
        if (loadArena.enabled)
        {
            
            
            timer += Time.deltaTime;
        
            if (timer >= gameTime+0.5)
            {
            
                stopTimer = true;
                player.SetActive(true);
                ground.SetActive(true);
                loadArena.gameObject.SetActive(false);
                player.transform.position = destination.transform.position;


            }

            if (!stopTimer)
            {
                timerSlider.value = timer;
            }
        
        }
    }
}

