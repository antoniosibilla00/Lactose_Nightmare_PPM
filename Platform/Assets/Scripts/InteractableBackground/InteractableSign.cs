using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSign : MonoBehaviour,Interactable
{
   
    [SerializeField] private string _prompt;
    [SerializeField] private Dialogue _dialogue;
    
    
    


    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;
    public Vector3 position => GetComponent<Transform>().position;

    public void Interact(Interactor interactor)
    {
        if (dialogue != null)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }

        

    }
}
