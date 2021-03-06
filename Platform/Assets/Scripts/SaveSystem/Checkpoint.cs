using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour,Interactable
{
    public GameMaster gm;
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
    
    private void Start()
    { 
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    
    
    


    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;

    public void Interact(Interactor interactor)
    {
       
       interactor.GetComponent<HealthSystem>().RestoreHealthAndPotions();
       interactor.GetComponent<PlayerScript>().SavePlayer();
        gm.lastCheckPointPos = transform.position;
        if (_dialogue != null)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }

        

    }
}
