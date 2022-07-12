using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class DialogueManager : MonoBehaviour
{
    private bool answer;
    private bool checkDebuffOrBuff = false ;
    private bool done = false ;
    // Start is called before the first frame update
    private Queue<string>sentences;
    
    [SerializeField] private  Text nameText;
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

    private AudioSource _audioSource;
    [SerializeField]private AudioClip buff;
    [SerializeField]private AudioClip debuff;

    
    //[SerializeField] private GameObject DialogueBox;
    private void Start()
    {
        sentences = new Queue<string>();
        
        
        RandomPosition(foodChose1, foodChose2);
        foodChose1.gameObject.SetActive(false);
        foodChose2.gameObject.SetActive(false);

        _audioSource = GetComponent<AudioSource>();


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
            Debug.Log("<<<dialogsentences.Count = "+sentences.Count);
            if (speaker.Equals("Tentatore") && sentences.Count == questionPosition )
            {
                
                
                
                //foodChose1.gameObject.GetComponentInChildren<Text>().text =  FindObjectOfType<Temptet>().GetFood1(); 
                //foodChose2.gameObject.GetComponentInChildren<Text>().text =  FindObjectOfType<Temptet>().GetFood2(); 
                
                
                foodChose1.gameObject.SetActive(true);
                foodChose2.gameObject.SetActive(true);

                Debug.Log("*dialogGetFood1 = " + foodChose1.gameObject.GetComponentInChildren<Text>().text);
                Debug.Log("*dialogGetFood2 = " + foodChose2.gameObject.GetComponentInChildren<Text>().text);
                
                
                Continue.gameObject.SetActive(false);

                
                
            }else if (speaker.Equals("Tentatore") && sentences.Count == questionPosition-1)
            {
                done = true;

                GameObject.FindGameObjectWithTag("Tempter").GetComponent<Temptet>().SetTrueitSTimeToOpen();
                GameObject.FindGameObjectWithTag("Tempter").GetComponent<Temptet>().OpenFood();
                
                if (answer )
                {

                    string sentence = "Per questa volta l'hai scampata, la mia sete non è stata sfamata ma non son un bugiardo,"+"\n"+" tieni questo buf ma spero che alla prossima mangerai del lardo";
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
                    string sentence = "La tua anima si avvicina, sento la sua rinascita vicina"+"2n"+"ti sembrerò un po lesto , tieni questo debuff, spero ci vedremo presto";
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
            Debug.Log("<<<dialogo.qp = "+dialogue.questionPosition);
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
            { GameObject.FindGameObjectWithTag("Tempter").GetComponent<Temptet>().CloseFood();;
                
            }
            else if(speaker.Equals("Re")){

                Debug.Log(">>>>Sono entrato DialogMan");
                GameObject.FindGameObjectWithTag("King").GetComponent<Temptet>().SetTrueTeleport();
               
                
            }else if (speaker.Equals("Re."))
            {
                SceneManager.LoadScene("Outro");
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
        _audioSource.clip = buff;
        _audioSource.Play();
        DisplayNextSentence();

    }

    public void  WrongAnswer()
    {
        _audioSource.clip = debuff;
        _audioSource.Play();
        answer = false;
        DisplayNextSentence();


    }

    public void RandomPosition(Button food1, Button food2)
    {
        Vector3 temp;

        int randomNum = Random.Range(0, 1);

        if (randomNum == 1)
        {

            temp = food1.transform.position;

            food1.transform.position = food2.transform.position;

            food2.transform.position = temp;

        }
        
        


    }
        
    
}
