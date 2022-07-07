using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageChangeLevel : MonoBehaviour
{
    public GameObject levelManager;
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
        ChangeSceneButton changeSceneButton = new ChangeSceneButton();
      

        Instantiate(levelManager);
        
        level1.SetActive(false);
        
         GameObject canvas =levelManager.transform.GetChild(0).gameObject;
         
         canvas.SetActive(true);

         changeSceneButton.ChangeScene("level2");
    }
}
