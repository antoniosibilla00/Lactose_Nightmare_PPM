using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temptet : MonoBehaviour,Interactable
{
    
    [SerializeField] private int Question;
    [SerializeField] private String _prompt;
    [SerializeField] private Dialogue _dialogue;
    [SerializeField] private String food1;
    [SerializeField] private String food2;
    
    private Vector3 destinationPosition;
    [SerializeField] private GameObject destination;
    [SerializeField] private Transform player;


    [SerializeField] public GameObject food;
    
    private bool openFood = true;
    private bool itSTimeToOpen;
    public bool teleport;
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
        
        if (dialogue.name == "Tentatore")
        {
            food.SetActive(foodBool);
            
            if (itSTimeToOpen)
            {
            
                anim.SetBool("openFood", openFood);
            
            }
        }
        else if(dialogue.name == "Re")
        {
            Debug.Log(">>>>Teleport = " +GetTrueTeleport());
            anim.SetBool("teleport",GetTrueTeleport());
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
        Debug.Log((">>>>TeleportTruePRe + ") + teleport);
        teleport = true;
        Debug.Log((">>>>TeleportTruePost + ") + teleport);
    }

    public void SetActiveFood()
    {

        foodBool = true;

    }
    public bool GetTrueTeleport()
    {
        
        return teleport ;
        
    }
    
    public void SetInvisibleFood()
    {

        foodBool = false;

    }

    public string GetFood1()
    {
        Debug.Log("*TemptetGetFood1 = " + food1);
        return food1;

    }

    public string GetFood2()
    {
        Debug.Log("*TemptetGetFood2 = " + food2);
        return food2;

    }
    

}
