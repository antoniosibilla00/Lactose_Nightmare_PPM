using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temptet : MonoBehaviour,Interactable
{
    
    [SerializeField] private int Question;
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;

    public void Interact(Interactor interactor)
    {
        if (_dialogue != null)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }
    }
}
