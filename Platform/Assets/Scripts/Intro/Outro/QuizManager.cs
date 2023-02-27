using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UIElements.Button;
using  System.Linq;

using UnityEngine.Rendering;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public Question[] questions;

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField] private Text factText;
    [SerializeField] private float timeBetweenQuestions = 0.5f;
    
    [SerializeField] public GameObject endButton ;
    
    [SerializeField] public GameObject trueButton ;
    [SerializeField] public GameObject falseButton ;
    
    [SerializeField] private int indexMaxQuestion ;


    public int indexElementToSave = 0 ;
    
    private ArrayList  elementToSave = new ArrayList();
    
    private int indexQuestion = 0;
    private void Start()
    {
       
        Debug.Log("Prima/indexQuestion  " + indexQuestion);


       

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

     
        SetCurrentQuestion();
        
        
        
      //  Debug.Log(elementToSave[indexElementToSave] + "/ index : " + indexElementToSave);
        Debug.Log(currentQuestion.fact + "/ is " + currentQuestion.isTrue);

        
            
    }


    void SetCurrentQuestion()
    {
        
       
        
        if (indexQuestion<indexMaxQuestion)
        {
            
            

            int ramdomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
            currentQuestion = unansweredQuestions[ramdomQuestionIndex];

            factText.text =  currentQuestion.fact;
                
            ++indexQuestion;
        
            elementToSave.Insert(indexElementToSave,currentQuestion.fact); 

            ++indexElementToSave;
            
            //Debug.Log("/indexQuestion  " + indexQuestion);

           

            //aveDataInTxtFile(factText.text);







            //unansweredQuestions.RemoveAt(ramdomQuestionIndex);

        }else
        {

            factText.text = "Bene hai completato tutto ";
            trueButton.gameObject.SetActive(false);
            falseButton.gameObject.SetActive(false);
            endButton.gameObject.SetActive(true);

            SaveDataInTxtFile();

        }

    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion);
        
        yield return new WaitForSeconds(timeBetweenQuestions);
        
        

        SetCurrentQuestion();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    
    public void UserSelectTrue()
    {

        if (currentQuestion.isTrue)
        {
            
            Debug.Log("/CORRECT");
            //elementToSave[indexElementToSave] = "Risposta: Vero (Corretta)";
            
            elementToSave.Insert(indexElementToSave,"Risposta: Vero (Corretta)"); 

            ++indexElementToSave;
        }
        else
        {
            
            Debug.Log("/WRONG");
            
            //elementToSave[indexElementToSave] = "Risposta: Vero (Sbagliata)";
            elementToSave.Insert(indexElementToSave,"Risposta: Vero (Sbagliata)"); 

            ++indexElementToSave;
        }

        StartCoroutine(TransitionToNextQuestion());

    }
    
    public void UserSelectFalse()
    {

        if (!currentQuestion.isTrue)
        {
            
            Debug.Log("/CORRECT");
            
            //elementToSave[indexElementToSave] = "Risposta: Falso (Giusta)";
            elementToSave.Insert(indexElementToSave,"Risposta: Falso (Giusta)"); 

            ++indexElementToSave;
            
        }
        else
        {
            
            Debug.Log("/WRONG");
            
            //elementToSave[indexElementToSave] = "Risposta: Falso (Sbagliata)";
            elementToSave.Insert(indexElementToSave,"Risposta: Falso (Sbagliata)"); 
            ++indexElementToSave;
            
        }
        
        StartCoroutine(TransitionToNextQuestion());
        
    }

    private void SaveDataInTxtFile()
    {
        //string path = Application.dataPath + ;
        
        StreamWriter writer = new StreamWriter( "Assets/TxtFile/Esito.txt", true);

        for (int i = 0; i < indexElementToSave ; i++)
        {

            if (i < indexElementToSave-1)
            {
                Debug.Log("Elemento " + i + elementToSave[i]);
                writer.Write(elementToSave[i] + "\n");
            }else 
            {
                Debug.Log("Elemento " + i + elementToSave[i]);
                writer.Write(elementToSave[i] + "\n");
                writer.Write("\n" + "---------------------------------------------------------------" +"\n");
                
            }
            
            
        }
        
        writer.Close();
        
    }
    
    
}
