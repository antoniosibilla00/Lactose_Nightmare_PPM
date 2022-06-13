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
    
    
    // Start is called before the first frame update
    public PlayerData(PlayerScript player)
    {
        health = player.healthSystem.GetCurrentHealth();
        level = player.level;
        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

    }
}
