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
    public GameObject arenaUiTimedWord;
    public GameObject Enemy2Prefab;
    protected GameObject Enemy1Pos;
    protected GameObject Enemy2Pos;
    public Tilemap tilemap;
    protected Text[] temp; 
    public static int enemiesCounter;
    protected GameObject myNewGameObject;
    protected GameObject myNewGameObject2;
    protected bool startArena;
    private bool done;
    //private const String CHOCOLATE_WITCH = "ChocolateWitch";
    //private const String SALAMI_DOG ="SalamiDogGO" ;
    // Start is called before the first frame update
    void Start()
    {
        
        round = 0;
        triggered = false;
        done = false;
        AudioSource = GetComponent<AudioSource>();
        Enemy1Pos = gameObject.transform.GetChild(0).gameObject;
        Enemy2Pos = gameObject.transform.GetChild(1).gameObject;
        startArena = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("arenaManagement.enemiesCounter " + enemiesCounter);
        if (triggered && startArena)
        {
            Debug.Log("arena.isTriggered"); 
            
            if (round == 0 && !done )
            {
                done = true;
                //sium//
                
                SpawnEnemies(round);
                MusicManager.istance.PlayArenaOst();
              
            }

            else if (round < 5 )
            {
                if (!temp[1].text.Equals("Nemici rimasti: " + enemiesCounter))
                {
                    temp[1].text = ("Nemici rimasti: " + enemiesCounter);
                }

                if (AreKilled())
                {
                    Debug.Log("spawnEnemies");
                    round++;
                    SpawnEnemies(round);

                }
            }
            else
            {
                AudioSource.clip = victorySound;
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
                arenaUi= Instantiate(arenaUi, new Vector3(-601f,-123f,0), arenaUi.transform.rotation);
                arenaUi.transform.SetParent(alexanderUI.transform , false); 
                temp = arenaUi.GetComponentsInChildren<Text>();
                StartCoroutine (SetTimedUi ( 3.0f)); //Wait 3 seconds then show UI
                StartCoroutine (ChangePhraseInTimedUi( 1.50f,"Sconfiggi tutti i nemici per avanzare"));
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
    
     IEnumerator SetTimedUi ( float secondsToWait)
     {
         Debug.Log("HideGuiprima");
         arenaUiTimedWord.SetActive (true);
         Text timedPhrase = arenaUiTimedWord.GetComponentInChildren<Text>();
         timedPhrase.text = "Benvenuto nell'arena";
         yield return new WaitForSeconds (secondsToWait);
         Debug.Log("HideGuidopo");
         arenaUiTimedWord.SetActive (false);
         startArena = true;

     }
     
     IEnumerator ChangePhraseInTimedUi ( float secondsToWait, String newPhrase)
     {
         Text timedPhrase = arenaUiTimedWord.GetComponentInChildren<Text>();
         yield return new WaitForSeconds (secondsToWait);
         timedPhrase.text = newPhrase;
         // GENERARE DINAMICAMENTE

     }
}
