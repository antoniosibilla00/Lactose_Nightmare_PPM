using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DoorArenaOpen : MonoBehaviour, Interactable
{
    [SerializeField] private Transform tpPoint;
    [SerializeField] private String _prompt;
    
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;
    public void Interact(Interactor interactor)
    {
        interactor.transform.position = new Vector3(tpPoint.position.x,tpPoint.position.y+0.5f,0);
    }
}
