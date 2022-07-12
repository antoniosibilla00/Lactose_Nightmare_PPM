using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHouse : MonoBehaviour,Interactable
{
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private String _interactionPrompt;
        
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string interactionPrompt => _interactionPrompt;
    public Dialogue dialogue => _dialogue;
    public void Interact(Interactor interactor)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
