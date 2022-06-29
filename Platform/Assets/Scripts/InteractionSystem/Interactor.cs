using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactionPointRadius;
    private readonly Collider2D []_collider = new Collider2D[3];
    [SerializeField] private Text textToDisplay;
    [FormerlySerializedAs("panelToDisplay")] [SerializeField] private GameObject interactionPanel;
    [SerializeField] private GameObject dialoguePanel;
    private bool done = false;

    private void Update()
    {
        Debug.Log("done  =  " + done);
        int numFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position,interactionPointRadius,_collider,interactableMask);
        
        if (numFound >0)
        {
            if (!done)
            {
                Debug.Log("Sono entrato sium");
                interactionPanel.SetActive(true);
            }
            
            var interactable = _collider[0].GetComponent<Interactable>();
            
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactionPanel.SetActive(false);
                done = true;
                
                if (dialoguePanel != null)
                {
                    dialoguePanel.SetActive(true);
                }
                
                
                interactable.Interact(this);
            }
            
            if (!interactable.interactionPrompt.Equals(null))
            {
                textToDisplay.text = interactable.interactionPrompt;
                
            }
        }
        else
        {
            interactionPanel.SetActive(false);
            done = false;
        }
        
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position,interactionPointRadius);
    }
    
}
