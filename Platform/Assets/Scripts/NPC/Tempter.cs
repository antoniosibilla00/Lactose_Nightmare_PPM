using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tempter : MonoBehaviour,Interactable
{
    
    [SerializeField] private static int Question;
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
     public  String food1;
     public String food2;
    
    private Vector3 destinationPosition;

    [SerializeField] public GameObject food;
    
    private bool foodBool;    
    public Animator Anim;
    public  bool done = false;
  

    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;
    public Vector3 position => GetComponent<Transform>().position;
    public static Tempter instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        Anim = GetComponent<Animator>();


    }

    // Update is called once per frame

    
    public void Interact(Interactor interactor)
    {
        if (_dialogue != null &&!done)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
            done = true;
        }
    }
    
    public void DestroyTempter()
    {
        
        Destroy(gameObject);
        
    }
    
    public void SetActiveFood()
    {

        foodBool = true;

    }

    public void SetInvisibleFood()
    {

        foodBool = false;

    }

    public string GetFood1()
    {
        return food1;

    }

    public string GetFood2()
    {
      
        return food2;

    }
    
    
    

}
