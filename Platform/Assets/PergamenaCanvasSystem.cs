using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PergamenaCanvasSystem : MonoBehaviour
{
    public PergamenaSystem PergamenaSystem;
    private void Start()
    {
        this.GetComponentInChildren<Text>().text = FindObjectOfType<PergamenaSystem>().getTextByPergamena();
       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) ||  Input.GetKeyDown(KeyCode.Space) )
        {
             Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
            Time.timeScale = 1;


        }
    }
}
