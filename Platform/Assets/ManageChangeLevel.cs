using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageChangeLevel : MonoBehaviour
{
    private bool done;
    public  GameObject canvas;
    public GameObject level1;
    // Start is called before the first frame update
    void Start()
    {
        done = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("doneCaricamento"+done);
        
        
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        // GameObject levelmanagerClone = GameObject.Instantiate(levelManager);
       
       
        if (!done)
        {
            level1.SetActive(false);
            done=true;
            PlayerScript.instance.transform.position=new Vector3(-56.93f, 18.266f, 0f);
            PlayerScript.instance.level = 2;
            SaveSystem.SavePlayer(PlayerScript.instance);
            LevelLoader.instance.LoadScene(2);
            //GameMaster.instance.lastCheckPointPos = new Vector3(-56.93f, 18.266f, 0f);
            
           
        }
       
      

    
        

    }
}
