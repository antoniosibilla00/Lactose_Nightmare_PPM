using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,Interactable
{
    [SerializeField] private string _prompt;
    public GameObject level2;
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
    public Vector3 position => GetComponent<Transform>().position;

    public void Interact(Interactor interactor)
    {
       
        level2.SetActive(false);
        PlayerScript.instance.transform.position=new Vector3(-179.23f, -0.445f, 0f);
        PlayerScript.instance.level = 3;
        SaveSystem.SavePlayer(PlayerScript.instance);
        LevelLoader.instance.LoadScene(3);
    }
}
