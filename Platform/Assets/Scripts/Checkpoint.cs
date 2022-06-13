using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour,Interactable
{
    public GameMaster gm;
    [SerializeField] private String _prompt;
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    
    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthSystem>().RestoreHealthAndPotions();
            gm.lastCheckPointPos = transform.position;
           //SaveSystem.SavePlayer();
        }
    }
    */


    public string interactionPrompt => _prompt;
    public void Interact(Interactor interactor)
    {
       interactor.GetComponent<HealthSystem>().RestoreHealthAndPotions();
       interactor.GetComponent<PlayerScript>().SavePlayer();
        gm.lastCheckPointPos = transform.position;
        Debug.Log("checkpoint");
    }
}
