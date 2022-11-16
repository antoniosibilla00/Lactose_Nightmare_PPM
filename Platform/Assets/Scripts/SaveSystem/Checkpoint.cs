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
    public Vector3 position => GetComponent<Transform>().position;

    public void Interact(Interactor interactor)
    {
       
       HealthSystem.Instance.RestoreHealthAndPotions();
       PlayerScript.instance.SavePlayer();
       RestoreAllEnemies();
       gm.lastCheckPointPos = transform.position;
        if (_dialogue != null)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }
        
    }
    
    private void RestoreAllEnemies()
    {
        var enemies = GameObject.Find("Enemies");
        var numChild = enemies.transform.childCount;
        for(var i=0;i<numChild;i++)
        {
           var tempEnemy= enemies.transform.GetChild(i);
           tempEnemy.GetComponentInChildren<EnemiesHealthSystem>().RestoreHealth();
        }
    }
}
