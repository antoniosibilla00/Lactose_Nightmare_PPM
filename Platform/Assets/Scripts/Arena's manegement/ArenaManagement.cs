using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public abstract class ArenaManagement : MonoBehaviour
{
    private bool triggered;

    private int round;
    public AudioClip roundSound;
    public AudioClip victorySound;
    protected AudioSource AudioSource;
    public GameObject alexanderUI;
    public GameObject Enemy1Prefab;
    public GameObject arenaUi;
    public GameObject Enemy2Prefab;
    protected GameObject Enemy1Pos;
    protected GameObject Enemy2Pos;
    public Tilemap tilemap;
    protected Text[] temp; 
    public static int enemiesCounter;
    protected GameObject myNewGameObject;
    protected GameObject myNewGameObject2;
    private bool done;
    //private const String CHOCOLATE_WITCH = "ChocolateWitch";
    //private const String SALAMI_DOG ="SalamiDogGO" ;
    // Start is called before the first frame update
    void Start()
    {

        round = 0;
        done = false;
        AudioSource = GetComponent<AudioSource>();
        Enemy1Pos = gameObject.transform.GetChild(0).gameObject;
        Enemy2Pos = gameObject.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("arenaManagement.enemiesCounter "+enemiesCounter);
        if (triggered)
        {
           
            
            if (round == 0 && !done)
            { 
                done = true;
                SpawnEnemies(round);
                MusicManager.istance.PlayArenaOst();
            }
            
            else if (round <5)
            {
                if (!temp[1].text.Equals("Nemici rimasti: " + enemiesCounter))
                {
                    temp[1].text = ("Nemici rimasti: " + enemiesCounter);
                }
                if (AreKilled())
                {
                    round++;
                    SpawnEnemies(round);
                    
                }
            }
            else
            {
                AudioSource.clip=victorySound;
                AudioSource.Play();
                MusicManager.istance.PlayMainOst();
                
                DeleteElements();
                Destroy(arenaUi.gameObject);
                Destroy(this.gameObject);
            }
            
        }  
    }


    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.CompareTag("Player"))
        {
        
            if (triggered != true)
            {
                arenaUi= Instantiate(arenaUi, new Vector3(1101,938,0), arenaUi.transform.rotation);
                arenaUi.transform.parent = alexanderUI.transform; 
                temp = arenaUi.GetComponentsInChildren<Text>();
                
            }
            triggered = true;
          
        }
      
    }


    public abstract void SpawnEnemies(int round);
    
    private bool AreKilled()
    {
   
       return myNewGameObject==null && myNewGameObject2==null; /*(Enemy1Pos.GetComponentInChildren<Transform>().childCount <= 0 && Enemy2Pos.GetComponentInChildren<Transform>().childCount <= 0)*/

    }


    
    
    
    
    /*
    private bool EnemiesUpdater()
    {
   
        return (Enemy1Pos.GetComponentInChildren<Transform>().childCount <= 0 && Enemy2Pos.GetComponentInChildren<Transform>().childCount <= 0);

    }
    */
    protected abstract void DeleteElements();
    

     protected void PlayRoundSound()
    {
        AudioSource.clip = roundSound;
        AudioSource.Play();
    }
    
}
