using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int health;

    public int level;

    public float[] position;

    
    public List<int> collectedScrolls; 
    // Start is called before the first frame update
    public PlayerData(PlayerScript player)
    {
        health = player.healthSystem.GetCurrentHealth();
        level = player.level;
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        collectedScrolls = player.indexSrollsCollected;

    }

    public PlayerData(int health, int level, float positionX, float positionY,List<int> TempCollectedScrolls)
    {
        
        this.health = health;
        this.level = level;
        position = new float[2];
        position[0] = positionX;
        position[1] = positionY;
        collectedScrolls = TempCollectedScrolls;

    }
    
    
    
    
}
