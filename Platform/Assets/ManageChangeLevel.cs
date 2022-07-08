using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;
using UnityEngine;

public class ManageChangeLevel : MonoBehaviour
{
    
    public  GameObject levelManager;
    public GameObject level1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        
        
       // GameObject levelmanagerClone = GameObject.Instantiate(levelManager);
        
        
       

        GameObject canvas = levelManager.transform.GetChild(0).gameObject;
        
        canvas.SetActive(true);
        
        level1.SetActive(false);
        
        GameMaster.instance.lastCheckPointPos = new Vector3(-56.93f,18.266f,0f);
        
        

        levelManager.GetComponentInChildren<LevelLoader>().LoadScene("Level2");




    }
}
