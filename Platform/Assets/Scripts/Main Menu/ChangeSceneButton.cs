using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
   
    // Start is called before the first frame update

    public void ChangeScene(string sceneName)
    {

        
        LevelLoader.instance.LoadScene( sceneName);
    
        
    }
    
    public void LoadPlayer()
    {
        PlayerData player= SaveSystem.LoadPlayer();
        if (player != null)
        {
            LevelLoader.instance.LoadScene( player.level);
        }
    }

}
