using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractableDoor : MonoBehaviour,Interactable
{
   
    [SerializeField] private String _prompt;
    [SerializeField] private Transform tpPoint;
    // Start is called before the first frame update
   
    [SerializeField] public GameObject alexanderUI;
    
    [SerializeField] private GameObject CanvasArenaPrefab;
    protected GameObject myNewGameObject;
    
    protected Interactor game;
    
    private bool todo = true;
    
    private string textToShow = "Sul cartello viene raffigurata un'incisione, sembra essere richiesta una  parola magica:\n" + "\nIn quali soggetti è molto comune che siano anche intolleranti al lattosio ?";
    
    private  float time = 1.5f;
    public string interactionPrompt => _prompt;
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;


    public void Interact(Interactor interactor)
    {
        
        if (todo)
        {
            
            myNewGameObject = Instantiate(CanvasArenaPrefab, CanvasArenaPrefab.transform.position , CanvasArenaPrefab.transform.rotation);
            myNewGameObject.transform.SetParent(alexanderUI.transform , false); 
            
            myNewGameObject.GetComponentInChildren<TMP_InputField>().ActivateInputField();

            myNewGameObject.GetComponentInChildren<Text>().text = textToShow;
            
            Time.timeScale = 0;
            
        }
        else
        {
            interactor.transform.position = new Vector3(tpPoint.position.x,tpPoint.position.y+0.5f,0);
        }


        game = interactor;
    }
    
    public void InsertWordCorrect()
    {
        
        Debug.Log("CorrectWord");
        
        
        game.transform.position = new Vector3(tpPoint.position.x,tpPoint.position.y+0.5f,0);
        
        Time.timeScale = 1;

        todo = false;

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

       
        
       if ( myNewGameObject.GetComponentInChildren<TMP_InputField>().text.Equals("celiaci", StringComparison.InvariantCultureIgnoreCase))
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

        myNewGameObject.GetComponentInChildren<Text>().text = textToShow;

        
        myNewGameObject.GetComponentInChildren<TMP_InputField>().ActivateInputField();
        

    }
    
}
