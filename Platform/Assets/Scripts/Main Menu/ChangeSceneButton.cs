using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton
{
    // Start is called before the first frame update
    

    public void ChangeScene(string sceneName)
    {
        LevelLoader.instance.LoadScene( sceneName);
    }
}
