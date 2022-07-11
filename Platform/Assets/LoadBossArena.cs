using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadBossArena : MonoBehaviour,Interactable
{
    [SerializeField] public Canvas loadArena;
    

    [SerializeField] private string _prompt;

    private string _interactionPrompt;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string interactionPrompt =>_prompt;


    public Dialogue dialogue => null;

    public void Interact(Interactor interactor)
    {
        
        loadArena.gameObject.SetActive(true);
        MusicManager.istance.PlayArenaOst();
    }
}