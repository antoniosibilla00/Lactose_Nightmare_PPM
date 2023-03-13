using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
public class LoadBossArena : MonoBehaviour,Interactable
{
    [SerializeField] public Canvas loadArena;
    

    [SerializeField] private string _prompt;

    private string _interactionPrompt;

    // Start is called before the first frame update

    private string textToShow = "Sul cartello viene raffigurata un'incisione, sembra essere richiesta una  parola magica:\n" + "\nQuali alimenti ti permettono di compensare la carenza di calcio ?";

    
    public string interactionPrompt =>_prompt;

    private bool todo = true;

    [SerializeField] public GameObject alexanderUI;
    
    [SerializeField] private GameObject CanvasArenaPrefab;
    protected GameObject myNewGameObject;
    
    public Dialogue dialogue => null;
    public Vector3 position => GetComponent<Transform>().position;
    
    private  float time = 1.5f;

    public void Interact(Interactor interactor)
    {
        
        /*loadArena.gameObject.SetActive(true);
        MusicManager.istance.PlayArenaOst();*/
        
        if (todo)
        {
            
            myNewGameObject = Instantiate(CanvasArenaPrefab, CanvasArenaPrefab.transform.position , CanvasArenaPrefab.transform.rotation);
            myNewGameObject.transform.SetParent(alexanderUI.transform , false); 
            
            myNewGameObject.GetComponentInChildren<TMP_InputField>().ActivateInputField();
            
            myNewGameObject.GetComponentInChildren<Text>().text = textToShow;
            
            Time.timeScale = 0;
            
        }
        
        
    }
    public void InsertWordCorrect()
    {
        
        Debug.Log("CorrectWord");
        
        loadArena.gameObject.SetActive(true);
        MusicManager.istance.PlayArenaOst();
       
        
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

       
        
       if ( myNewGameObject.GetComponentInChildren<TMP_InputField>().text.Equals("Verdure", StringComparison.InvariantCultureIgnoreCase)|| myNewGameObject.GetComponentInChildren<TMP_InputField>().text.Equals("Verdura", StringComparison.InvariantCultureIgnoreCase))
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

