using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour,Interactable
{
    public GameMaster gm;
    [SerializeField] private String _prompt;
    [SerializeField] private Transform tpPoint;
    // Start is called before the first frame update
   
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;


    public void Interact(Interactor interactor)
    {
        interactor.transform.position = new Vector3(tpPoint.position.x,tpPoint.position.y+0.5f,0);
    }
}
