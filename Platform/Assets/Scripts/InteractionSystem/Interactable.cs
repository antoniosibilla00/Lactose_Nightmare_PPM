using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    // Start is called before the first frame update
    public string interactionPrompt
    {
        get;
    }
    
    
    public Dialogue dialogue 
    {
        get;
    }
    
    public Vector3 position 
    {
        get;
    }

    public void Interact(Interactor interactor);
    
    }
