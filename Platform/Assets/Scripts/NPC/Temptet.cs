using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temptet : MonoBehaviour,Interactable
{
    
    [SerializeField] private int Question;
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
    private Vector3 destinationPosition;
    [SerializeField] private GameObject destination;
    [SerializeField] private Transform player;


    [SerializeField] public GameObject food;
    
    private bool openFood = true;
    private bool itSTimeToOpen;
    private bool teleport = false;
    private bool foodBool = false;    
    public Animator anim;
    
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => _dialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if(dialogue.name == "Porta")
        {
            destinationPosition = destination.transform.position;
        }

        
        anim = GetComponent<Animator>();
        itSTimeToOpen = false;
        
      
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (dialogue.name == "tentatore")
        {
            food.SetActive(foodBool);
            
            if (itSTimeToOpen)
            {
            
                anim.SetBool("openFood", openFood);
            
            }
        }
        else if(dialogue.name == "Re")
        {
            anim.SetBool("teleport", teleport);
        }



    }
    
    
 

    public void Interact(Interactor interactor)
    {
        if (_dialogue != null)
        {
            FindObjectOfType < DialogueManager>().StartDialogue(dialogue);
        }
    }
    
    
    public void OpenFood()
    {
       
        openFood = true;

    }
    
    public void CloseFood()
    {
       
        openFood = false;
    
    }

    public void SetTrueitSTimeToOpen()
    {
        
        itSTimeToOpen = true;

    }

    public void DestroyTempter()
    {
        
        Destroy(gameObject);
        
    }

    public void SetTrueTeleport()
    {
        
        teleport = true;
    }

    public void SetActiveFood()
    {

        foodBool = true;

    }
    
    public void SetInvisibleFood()
    {

        foodBool = false;

    }

 


}
