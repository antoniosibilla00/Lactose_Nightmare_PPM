using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private LayerMask interactableMask;
    [SerializeField]private GameObject AlexanderUi;
    [SerializeField] private float interactionPointRadius;
    private  Collider2D []_collider = new Collider2D[3];
    [SerializeField] private GameObject dialoguePanel;
    
    private bool done = false;
    private bool spawnedInteractionText = false;
    public static bool spawnedDialogue = false;
    
    GameObject tmpTextAboveEnemies = null;
    public GameObject textAboveInteractable;
    GameObject tmpDialoguePanel = null;
    Interactable interactable =null;
    private void Update()
    {

       
        var numFound = Physics2D.OverlapCircleNonAlloc(interactionPoint.position,interactionPointRadius,_collider,interactableMask);
        _collider=_collider.Distinct().ToArray();
        
        if (numFound >0)
        {
            if (!done)
            {
                interactable = _collider[0].GetComponent<Interactable>();
                if (interactable == null) return;
                if (!spawnedInteractionText )
                {
                    tmpTextAboveEnemies=Instantiate(textAboveInteractable, interactable.position, Quaternion.identity);
                    spawnedInteractionText = true;
            
                }else if (!tmpTextAboveEnemies.GetComponent<TextMeshPro>().text.Equals(interactable.interactionPrompt))
                {
                    Destroy(tmpTextAboveEnemies);
                    spawnedInteractionText = false;

                }
                
                
                //Debug.Log("tmpTextAboveEnemies.GetComponent<TextMeshPro>().text: "+tmpTextAboveEnemies.GetComponent<TextMeshPro>().text +"interactable.interactionPrompt: "+interactable.interactionPrompt);
            
                if (  Input.GetKeyDown(KeyCode.E) && PlayerScript.instance.state!=PlayerScript.State.death && !done)
                {
                    Destroy(tmpTextAboveEnemies);
                    done = true;
                    spawnedInteractionText = false;

                    if (interactable.dialogue!= null && !spawnedDialogue)
                    {
                        spawnedDialogue = true;
                        tmpDialoguePanel = Instantiate(dialoguePanel,new Vector3(959,96.83691f,0),dialoguePanel.transform.rotation);
                        tmpDialoguePanel.transform.SetParent( AlexanderUi.transform , false);
                    
                    

                    }
                
                
                    interactable.Interact(this);
                }
            }
            
            if ( !interactable.interactionPrompt.Equals(null) && tmpTextAboveEnemies!=null)
            {
                tmpTextAboveEnemies.GetComponent<TextMeshPro>().text = interactable.interactionPrompt;
            }


           

        }
        else
        {
            Destroy(tmpTextAboveEnemies);
            done = false;
            spawnedInteractionText = false;

        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position,interactionPointRadius);
    }
    
}
