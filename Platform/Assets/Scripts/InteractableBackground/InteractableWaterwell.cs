using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWaterwell : MonoBehaviour,Interactable
{
    public GameMaster gm;
    [SerializeField] private String _prompt;
    public GameObject[] cavesParallax;
    
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    
   

    public string interactionPrompt => _prompt;
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;


    public void Interact(Interactor interactor)
    {
        
       
        gm.lastCheckPointPos = transform.position;
        
        interactor.GetComponent<PlayerScript>().SavePlayer();
        interactor.transform.position = new Vector3(16.38f, 13.58f, 0f);
        
        foreach (var caveParallax in cavesParallax)
        {
            caveParallax.SetActive(true);
        }
        

    }
}
