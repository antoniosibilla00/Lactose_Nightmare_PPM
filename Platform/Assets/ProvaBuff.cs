using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaBuff : MonoBehaviour, Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string interactionPrompt { get; }
    public Dialogue dialogue { get; }
    public Vector3 position { get; }
    public void Interact(Interactor interactor)
    {
        PlayerScript.instance.Buff();
    }
}
