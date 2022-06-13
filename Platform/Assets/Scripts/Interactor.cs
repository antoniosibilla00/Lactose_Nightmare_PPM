using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField] private float interactionPointRadius;
    private readonly Collider2D []_collider = new Collider2D[3];

    private void Update()
    {
        
        int numFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position,interactionPointRadius,_collider,interactableMask);
        
        if (numFound >0)
        {

            var interactable = _collider[0].GetComponent<Interactable>();
            
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position,interactionPointRadius);
    }
    
}
