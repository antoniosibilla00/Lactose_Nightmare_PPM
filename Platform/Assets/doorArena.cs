using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class doorArena : MonoBehaviour,Interactable
{
    private SpriteRenderer spriteRenderer;

    private  float time = 1.5f;
     
    
    
    [SerializeField] public GameObject alexanderUI;
    
    [SerializeField] private GameObject CanvasArenaPrefab;
    protected GameObject myNewGameObject;
    
    [SerializeField] private String _prompt;
    
    private bool todo = true;
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;
    public void Interact(Interactor interactor )
    {
        if (todo)
        {
            
            myNewGameObject = Instantiate(CanvasArenaPrefab, CanvasArenaPrefab.transform.position , CanvasArenaPrefab.transform.rotation);
            myNewGameObject.transform.SetParent(alexanderUI.transform , false); 
            
            myNewGameObject.GetComponentInChildren<TMP_InputField>().ActivateInputField(); 
            
            Time.timeScale = 0;
        }
       
    }
    public void InsertWordCorrect()
    {
        
        //canvasdoorArena.gameObject.SetActive(false);
        
        DeleteWall();

        _prompt = "";
        
        Time.timeScale = 1;

        todo = false;

    }

    void DeleteWall()
    {

        Tilemap tilemap = GameObject.Find("Forest").GetComponent<Tilemap>();

        for (int i = 491; i < 496; i++)
        {
            tilemap.SetTile(new Vector3Int(i,28,0), null);
            tilemap.SetTile(new Vector3Int(i,27,0), null);
        }

    }
    
    private void Update()
    {
        if (myNewGameObject != null)
        {
            if (myNewGameObject.GetComponentInChildren<TMP_InputField>().text.Length > 6 )
            {

                Color color = Color.clear;
            
                myNewGameObject.GetComponentInChildren<TMP_InputField>().caretColor = color;
            
            }
            else
            {
            
                Color color = Color.white;
            
                myNewGameObject.GetComponentInChildren<TMP_InputField>().caretColor = color;
            
            }
            
            if (Input.GetKeyDown(KeyCode.Escape) )
            {
            
                //this.gameObject.SetActive(false);
                Time_timeScale1();
                Destroy(myNewGameObject);


            }
            
            if (Input.GetKeyDown(KeyCode.Return))
            {
            
                CorretInsertWord();
            
            }
            
        }
        
    }

    public void Time_timeScale1()
    {
        Time.timeScale = 1;
    }

    public void CorretInsertWord()
    {
        Debug.Log("//// " + myNewGameObject.GetComponentInChildren<TMP_InputField>().text);

       
        
       if ( myNewGameObject.GetComponentInChildren<TMP_InputField>().text.Equals("lattasi", StringComparison.InvariantCultureIgnoreCase))
        {
            
            InsertWordCorrect();
            Destroy(myNewGameObject);
            
        }
        else
        {

            WrongInsertWord();
            
            
        }
        
    }

    public void WrongInsertWord()
    {

        myNewGameObject.GetComponentInChildren<Text>().text = "\n Sbagliato, ritenta sarai piu fortunato";
        
        
        StartCoroutine(canvasSetActiveFalse());

    }
    
    
    IEnumerator canvasSetActiveFalse()
    {
       
        myNewGameObject.GetComponentInChildren<TMP_InputField>().text = "";
        
        yield return new WaitForSecondsRealtime(time);

        myNewGameObject.GetComponentInChildren<Text>().text = "Sul cartello viene raffigurata un'incisione, sembra essere richiesta una  parola magica:\n" + "\nQuale enzima manca in un soggetto intollerante al lattosio ?";

        
        myNewGameObject.GetComponentInChildren<TMP_InputField>().ActivateInputField();
        

    }
    
    
  

    
}
