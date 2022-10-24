using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour,Interactable
{
    // Start is called before the first frame update
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
    public Animator anim;
    private bool done;
    public static bool LastOne = false;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;
    public Vector3 position => GetComponent<Transform>().position;
    public void Interact(Interactor interactor)
    {   
        if (_dialogue != null && !done)
        {
            done=true;
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }
    }
    
    public void Destroy()
    {
        
        Destroy(gameObject);
        
    }

}
