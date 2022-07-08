using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena2Manegement2 : MonoBehaviour
{
    // Start is called before the first frame update
 private bool triggered;

    private int round;

    public GameObject bigBubbleGolem;
    public GameObject batBurger;
    public GameObject Enemy1Pos;
    public GameObject Enemy2Pos;
    public Grid grid;
    public Tilemap tilemap;
    //private const String CHOCOLATE_WITCH = "ChocolateWitch";
    //private const String SALAMI_DOG ="SalamiDogGO" ;
    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("round"+round);
        if (triggered)
        {
            if (round <5)
            {
                if (AreKilled())
                {
                    round++;
                    SpawnEnemies();
                    
                }
            }
            else
            {
                
                DeleteStone();
                Destroy(this.gameObject);
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
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                
                myNewGameObject= Instantiate(batBurger, Enemy2Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy2Pos.transform;
                
                
                break;
            case 1:
                
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy1Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                
                myNewGameObject2 = Instantiate(batBurger, Enemy2Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                
                
                break;
            case 2 :
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy1Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;

                myNewGameObject.GetComponent<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponent<ChocolateWitchAI>().GetCooldown()-0.5f);
                
                myNewGameObject2= Instantiate(bigBubbleGolem, Enemy2Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                
                myNewGameObject2.GetComponent<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponent<ChocolateWitchAI>().GetCooldown()-0.5f);
                break;
            case 3 :
                myNewGameObject= Instantiate(batBurger, Enemy1Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().SetSpeed( myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().GetSpeed()+0.25f);
                
                
                
                myNewGameObject2= Instantiate(batBurger, Enemy2Pos.transform.position, batBurger.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().SetSpeed( myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().GetSpeed()+0.25f);
                break;
            case 4 :
                
                myNewGameObject= Instantiate(bigBubbleGolem, Enemy1Pos.transform.position, bigBubbleGolem.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.GetComponent<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponent<ChocolateWitchAI>().GetCooldown()-0.5f);
                myNewGameObject.GetComponent<ChocolateWitchAI>().SetSpeed( myNewGameObject.GetComponent<ChocolateWitchAI>().GetSpeed()+0.25f);
                break;
        }
        
    }

    private bool AreKilled()
    {
   
       return (Enemy1Pos.GetComponentInChildren<Transform>().childCount <= 0 && Enemy2Pos.GetComponentInChildren<Transform>().childCount <= 0);

    }
    
    void DeleteStone()
    {
      
        tilemap.SetTile(new Vector3Int(532,16,0), null);
        tilemap.SetTile(new Vector3Int(533,16,0), null);
        tilemap.SetTile(new Vector3Int(532,17,0), null);
    }
}
