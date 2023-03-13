using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArenaPasswordLevel2 : MonoBehaviour
{
    public GameObject InteractableDoor;

    private  float time = 1.5f;
    [SerializeField] private TMP_InputField inputTextPro;
    
    
    private void Start()
    {
        
        inputTextPro.ActivateInputField();
        
    }

    private void OnEnable()
    {

        
        Time.timeScale = 0;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            
            //this.gameObject.SetActive(false);
            Time_timeScale1();
            Destroy(this.gameObject);


        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            
            CorretInsertWord();
            
        }

        
        
        if (inputTextPro.text.Length > 6 )
        {

            Color color = Color.clear;
            
            inputTextPro.caretColor = color;
            
        }
        else
        {
            
            Color color = Color.white;
            
            inputTextPro.caretColor = color;
            
        }
        
    }

    public void Time_timeScale1()
    {
        Time.timeScale = 1;
    }

    public void CorretInsertWord()
    {
        Debug.Log("////");


        if ( inputTextPro.text.Equals("lattasi", StringComparison.InvariantCultureIgnoreCase))
        {
            
          // InteractableDoor.InsertWordCorrect();
            Destroy(this.gameObject);
            
        }
        else
        {

            WrongInsertWord();
            
            
        }
        
    }

    public void WrongInsertWord()
    {

        this.gameObject.GetComponentInChildren<Text>().text = "\n Sbagliato, ritenta sarai piu fortunato";
        
        
        StartCoroutine(canvasSetActiveFalse());

    }
    
    
    IEnumerator canvasSetActiveFalse()
    {
       
        inputTextPro.text = "";
        
        yield return new WaitForSecondsRealtime(time);

        this.gameObject.GetComponentInChildren<Text>().text = "Per accedere all'arena seleziona la parola magica:\n" + "\nQuale enzima manca in un soggetto intollerante al lattosio ?";

        
        inputTextPro.ActivateInputField();
        

    }
}
