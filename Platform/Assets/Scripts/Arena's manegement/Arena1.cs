using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena1 : ArenaManagement
{
    // Start is called before the first frame update

    // Update is called once per frame

    public override void SpawnEnemies(int round)
    {
        switch (round)
     {
         case 0:
             temp[0].text = "Round: 0 di 4";
             PlayRoundSound();
             enemiesCounter = 1;
             myNewGameObject= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
             myNewGameObject.transform.parent = Enemy1Pos.transform;
             myNewGameObject.AddComponent<OnEnemyKill>();
             
             
             break;
         case 1:
           
             PlayRoundSound();
             
             temp[0].text = "Round 1 di 4";
             enemiesCounter = 2;
             
             myNewGameObject= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
             myNewGameObject.transform.parent = Enemy1Pos.transform;
             myNewGameObject.AddComponent<OnEnemyKill>();
             myNewGameObject2 = Instantiate(Enemy2Prefab, Enemy2Pos.transform.position, Enemy2Prefab.transform.rotation);
             myNewGameObject2.transform.parent = Enemy2Pos.transform;
             myNewGameObject2.AddComponent<OnEnemyKill>();
             
             
             break;
         case 2 :
             
             PlayRoundSound();
             
             temp[0].text = "Round 2 di 4";
             enemiesCounter = 2;
             
             myNewGameObject= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
             myNewGameObject.transform.parent = Enemy1Pos.transform;
             myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().GetCooldown()-0.5f);
             myNewGameObject.AddComponent<OnEnemyKill>();
             
             myNewGameObject2= Instantiate(Enemy1Prefab, Enemy2Pos.transform.position, Enemy1Prefab.transform.rotation);
             myNewGameObject2.transform.parent = Enemy2Pos.transform;
             myNewGameObject2.AddComponent<OnEnemyKill>();
             
             myNewGameObject2.GetComponentInChildren<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().GetCooldown()-0.5f);
             break;
         case 3 :
             PlayRoundSound();
             
             temp[0].text = "Round 3 di 4";
             enemiesCounter = 2;
             
             myNewGameObject= Instantiate(Enemy2Prefab, Enemy1Pos.transform.position, Enemy2Prefab.transform.rotation);
             myNewGameObject.transform.parent = Enemy1Pos.transform;
             myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().SetSpeed( myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().GetSpeed()+0.25f);
             
             myNewGameObject.AddComponent<OnEnemyKill>();
             
             myNewGameObject2= Instantiate(Enemy2Prefab, Enemy2Pos.transform.position, Enemy2Prefab.transform.rotation);
             myNewGameObject2.transform.parent = Enemy2Pos.transform;
             myNewGameObject2.GetComponentInChildren<MeleeEnemyAI>().SetSpeed( myNewGameObject.GetComponentInChildren<MeleeEnemyAI>().GetSpeed()+0.25f);
             
             myNewGameObject2.AddComponent<OnEnemyKill>();
            
             break;
         case 4 :
             PlayRoundSound();
             
             temp[0].text = "Round 4 di 4";
             enemiesCounter = 1;

             myNewGameObject= Instantiate(Enemy1Prefab, Enemy1Pos.transform.position, Enemy1Prefab.transform.rotation);
             myNewGameObject.transform.parent = Enemy1Pos.transform;
             myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().SetCooldown( myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().GetCooldown()-0.5f);
             myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().SetSpeed( myNewGameObject.GetComponentInChildren<ChocolateWitchAI>().GetSpeed()+0.25f);
             
             myNewGameObject.AddComponent<OnEnemyKill>();


             break;
     }
    }

    protected override void DeleteElements()
    {
        DeleteStone();

    }
    
    void DeleteStone()
    {
        tilemap.SetTile(new Vector3Int(532,16,0), null);
        tilemap.SetTile(new Vector3Int(533,16,0), null);
        tilemap.SetTile(new Vector3Int(532,17,0), null);
    }
}
