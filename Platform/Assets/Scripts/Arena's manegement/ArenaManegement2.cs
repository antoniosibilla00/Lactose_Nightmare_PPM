using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArenaManegement2 : MonoBehaviour
{
   private bool triggered;

    private int round;
    public AudioClip roundSound;
    public AudioClip victorySound;
    private AudioSource AudioSource;

    public GameObject bigBubbleGolem;
    public GameObject batBurger;
    public GameObject Enemy1Pos;
    public GameObject Enemy2Pos;
    public BoxCollider2D[] safeZones;
    public Grid grid;
    public Tilemap forest;
    public Tilemap obstacles;

    private bool done;

    private bool musicPlayed;
    //private const String CHOCOLATE_WITCH = "ChocolateWitch";
    //private const String SALAMI_DOG ="SalamiDogGO" ;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
        round = 0;
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        Debug.Log("round"+round);
        Debug.Log("sonoMorti?:"+AreKilled());
        Debug.Log("isTriggered"+triggered);
        if (triggered)
        {
            if (round == 0 && !done)
            {
                done = true;
                SpawnEnemies();
                MusicManager.istance.PlayArenaOst();
            }
            else if (round <5)
            {
              
                if (!AreKilled()) return;
              
             
                round++;
                SpawnEnemies();
            }
            else
            {
                
                DeleteStone();
                DeleteSpikes();
                //AudioSource.clip=victorySound;
                //AudioSource.Play();
                if (!AudioSource.isPlaying)
                {
                    MusicManager.istance.PlayMainOst();
                }
                
               

                if (!musicPlayed)
                {
                    AudioSource.Play();
                    musicPlayed = true;
                }
                

                if (!AudioSource.isPlaying)
                {
                    Destroy(this.gameObject);
                }
               
            }

           
            
        }  
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            triggered = true;
        }
    }


    private void SpawnEnemies()
    {
        GameObject myNewGameObject;
        GameObject myNewGameObject2;
        
        switch (round)
        {
            case 0:
                PlayRoundSound();
                EnableSafeZones();
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy1Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy2Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy2Pos.transform;
                
                break;
            case 1:

                PlayRoundSound();
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                
                myNewGameObject= Instantiate(batBurger, Enemy2Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy2Pos.transform;
                
                break;
            
            case 2 :

                PlayRoundSound();
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject= Instantiate(batBurger, Enemy2Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy2Pos.transform;
                
               changeEnemy1Pos( 0-5f);
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy1Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform; ; 

                break;
            
            case 3 :
                PlayRoundSound();
                changeEnemy1Pos(5f);
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(20);
                    
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy2Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy2Pos.transform;
                myNewGameObject.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(20);
                break;
            
            case 4 :
                PlayRoundSound();
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(20);
                myNewGameObject.GetComponentInChildren<FlyingEnemy>().SetSpeed( myNewGameObject.GetComponentInChildren<FlyingEnemy>().GetSpeed()+0.25f);
                break;
        }
        
    }

    private bool AreKilled()
    {
   
       return (Enemy1Pos.GetComponentInChildren<Transform>().childCount <= 0 && Enemy2Pos.GetComponentInChildren<Transform>().childCount <= 0);
  

    }

    void EnableSafeZones()
    {
        foreach (var safeZone in safeZones)
        {
            safeZone.enabled = true;
        }
    }
    
    void DeleteStone()
    {
      
        forest.SetTile(new Vector3Int(457,20,0), null);
        forest.SetTile(new Vector3Int(457,21,0), null);
        forest.SetTile(new Vector3Int(4582,20,0), null);
    }

    void DeleteSpikes()
    {
        for (int i = 0; i < 58; i++)
        {
            obstacles.SetTile(new Vector3Int(399+i,20,0), null);
        }
    }

    void changeEnemy1Pos(float offset)
    {
        
        Enemy1Pos.transform.position= new Vector2(Enemy1Pos.transform.position.x + offset,Enemy1Pos.transform.position.y );  
    }
    
    void PlayRoundSound()
    {
        AudioSource.clip = roundSound;
        AudioSource.Play();
    }
}


