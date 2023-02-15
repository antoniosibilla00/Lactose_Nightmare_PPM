using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Arena2 : ArenaManagement
{

    
    public BoxCollider2D[] safeZones;
    
    public Tilemap obstacles;

    private bool done;

    private bool musicPlayed;
    
    public override void SpawnEnemies(int round)
    {
        switch (round)
        {
            case 0:
                temp[0].text = "Round: 0 di 4";
                enemiesCounter = 2;
                PlayRoundSound();
                EnableSafeZones();
                
                myNewGameObject= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.AddComponent<OnEnemyKill>();
                
                myNewGameObject2= Instantiate(Enemy1Prefab, Enemy2Pos.transform.position, Enemy1Prefab.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                myNewGameObject2.AddComponent<OnEnemyKill>();
                
                break;
            case 1:
                temp[0].text = "Round 1 di 4";
                enemiesCounter = 2;
                PlayRoundSound();
                
                myNewGameObject= Instantiate(Enemy2Prefab, Enemy1Pos.transform.position, Enemy2Prefab.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.AddComponent<OnEnemyKill>();
                
                myNewGameObject2= Instantiate(Enemy2Prefab, Enemy2Pos.transform.position, Enemy2Prefab.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                myNewGameObject2.AddComponent<OnEnemyKill>();
                
                break;
            
            case 2 :
                temp[0].text = "Round 2 di 4";
                enemiesCounter = 2;
                PlayRoundSound();
                
                myNewGameObject= Instantiate(Enemy2Prefab, Enemy1Pos.transform.position, Enemy2Prefab.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.AddComponent<OnEnemyKill>();
                
                myNewGameObject2= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
                myNewGameObject2.transform.parent = Enemy1Pos.transform; ; 
                myNewGameObject2.AddComponent<OnEnemyKill>();

                break;
            
            case 3 :
                temp[0].text = "Round 3 di 4";
                enemiesCounter = 2;
                PlayRoundSound();

                myNewGameObject= Instantiate(Enemy2Prefab, Enemy1Pos.transform.position, Enemy2Prefab.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                myNewGameObject.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(20);
                myNewGameObject.AddComponent<OnEnemyKill>();
                    
                myNewGameObject2= Instantiate(Enemy1Prefab, Enemy2Pos.transform.position, Enemy1Prefab.transform.rotation);
                myNewGameObject2.transform.parent = Enemy2Pos.transform;
                myNewGameObject2.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(20);
                myNewGameObject2.AddComponent<OnEnemyKill>();
                break;
            
            case 4 :
                temp[0].text = "Round 4 di 4";
                enemiesCounter = 1;
                PlayRoundSound();
                
                myNewGameObject= Instantiate(Enemy2Prefab, Enemy1Pos.transform.position, Enemy2Prefab.transform.rotation);
                myNewGameObject.transform.parent = Enemy1Pos.transform;
                
                myNewGameObject.transform.GetChild(0).GetComponentInChildren<TriggerDamage>().SetDamage(40);
                myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().SetSpeed( myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().GetSpeed()+0.25f);
                myNewGameObject.AddComponent<OnEnemyKill>();
                break;
        }
        
    }

    protected override void DeleteElements()
    {
        DeleteStone();
        DeleteSpikes();
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
      
        tilemap.SetTile(new Vector3Int(457,20,0), null);
        tilemap.SetTile(new Vector3Int(457,21,0), null);
        tilemap.SetTile(new Vector3Int(4582,20,0), null);
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
    

}


