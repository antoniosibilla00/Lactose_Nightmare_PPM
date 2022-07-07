using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private bool answer;
    private bool checkDebuffOrBuff = false ;
    private bool done = false ;
    // Start is called before the first frame update
    private Queue<string>sentences;
    
    [SerializeField]private  Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    private string speaker = "";
    private float waitTime;
    private float timer = 20.0f;
    [SerializeField] private Button foodChose1;
    [SerializeField] private Button foodChose2;
    [SerializeField] private Button Continue;
    private int questionPosition;
    private Temptet tempter;
    

    
    //[SerializeField] private GameObject DialogueBox;
    private void Start()
    {
        sentences = new Queue<string>();
        
        
        
        foodChose1.gameObject.SetActive(false);
        foodChose2.gameObject.SetActive(false);
        


    }

    private void Update()
    {
        if (checkDebuffOrBuff)
        {
            Debug.Log("waitTime1 = " + waitTime );
            waitTime -= Time.deltaTime;

            if (waitTime <= 0)
            {
                checkDebuffOrBuff = false;
                FindObjectOfType<PlayerScript>().Restore();

            }
            
        }
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            DisplayNextSentence();
        }

        
   

        if (sentences != null && !done)
        {
            if (speaker.Equals("Tentatore") && sentences.Count == questionPosition )
            {
                foodChose1.gameObject.SetActive(true);
                foodChose2.gameObject.SetActive(true);
                Continue.gameObject.SetActive(false);

                foodChose1.gameObject.GetComponentInChildren<Text>().text = "Pizza";
                foodChose2.gameObject.GetComponentInChildren<Text>().text = "Pasta";
                
            }else if (speaker.Equals("Tentatore") && sentences.Count == questionPosition-1)
            {
                done = true;
                

                
                FindObjectOfType<Temptet>().SetTrueitSTimeToOpen();
                FindObjectOfType<Temptet>().OpenFood();
                
                if (answer )
                {

                    string sentence = "Bravo!";
                    FindObjectOfType<PlayerScript>().Buff();
                    foodChose1.gameObject.SetActive(false);
                    foodChose2.gameObject.SetActive(false);
                    Continue.gameObject.SetActive(true);
                    checkDebuffOrBuff = true;
                    dialogueText.text = sentence;
                    waitTime = timer;
                
                }
                else 
                {
                    string sentence = "Sbagliato!";
                    FindObjectOfType<PlayerScript>().Debuff();
                    foodChose1.gameObject.SetActive(false);
                    foodChose2.gameObject.SetActive(false);
                    Continue.gameObject.SetActive(true);
                    checkDebuffOrBuff = true;
                    dialogueText.text = sentence;
                    waitTime = timer;
                    
                    


                }
                

            } 
        }
        
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        if (dialogue.questionPosition != 0)
        {
            questionPosition = dialogue.questionPosition;
        }
        
        dialoguePanel.SetActive(true); 
        sentences.Clear();

        nameText.text = dialogue.name;
        speaker = nameText.text;

        for (int i = 1; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
        }

        dialogueText.text = dialogue.sentences[0];

        Debug.Log("Speaker" + speaker);

        Debug.Log("sentences1.Count = "  + sentences.Count);
        
    }
    
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if (speaker.Equals("Tentatore"))
            {
                FindObjectOfType<Temptet>().CloseFood();
                
            }
            else if(speaker.Equals("Re")){

                Debug.Log(">>>>Sono entrato DialogMan");
                FindObjectOfType<Temptet>().SetTrueTeleport();
                
            }
            EndDialogue();
            return;
        }

        Debug.Log("sentences.Count = "  + sentences.Count);
        

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;

        Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }

    public void  CorrectAnswer()
    {

        answer = true;
        
        DisplayNextSentence();

    }

    public void  WrongAnswer()
    { 

        answer = false;
        DisplayNextSentence();


    }

    
        
    
}
