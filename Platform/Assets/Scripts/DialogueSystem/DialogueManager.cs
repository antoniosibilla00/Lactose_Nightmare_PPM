using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;


public class DialogueManager : MonoBehaviour
{
    
    private bool done = false ;
    private AudioSource _audioSource;
    
    private ArrayList  elementToSave = new ArrayList();
    
    private int indexElementToSave = 0 ;
    
    #region DialogueManagement
    
    private string speaker = "";
    private Queue<string>sentences;
    [SerializeField] private  Text nameText;
    [SerializeField] private Text dialogueText;
    //[SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Button Continue;
    
    #endregion
  

    #region QuestionManagement
    
    private const int  QuestionPosition=1;
    [SerializeField] private GameObject foodChose1;
    [SerializeField] private GameObject foodChose2;
    private GameObject tmpFoodChose1;
    private GameObject tmpFoodChose2;

    [SerializeField]private AudioClip buff;
    [SerializeField]private AudioClip debuff;
    
    private bool canSkip;
   
    #endregion
    
    
    //[SerializeField] private GameObject DialogueBox;
    private void Start()
    {
        done = false;
        canSkip = true;
        _audioSource = GetComponent<AudioSource>();
        
        
    }

    private void Update()
    {
        if (!canSkip) return;
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        sentences = new Queue<string>();
        sentences.Clear();

        nameText.text = dialogue.name;
        
        speaker = nameText.text;
        
        for (int i = 1; i < dialogue.sentences.Length; i++)
        {
            sentences.Enqueue(dialogue.sentences[i]);
        }
        
        dialogueText.text = dialogue.sentences[0];
        
    }
    
    public void DisplayNextSentence()
    {
        
        if (sentences.Count == 0)
        {
            switch (speaker)
            {
                case "Re":
                    FindObjectOfType<King>().anim.SetBool("teleport",true);
                    if (SceneManager.GetActiveScene().name.Equals("Castle"))
                    {
                        if (King.LastOne)
                        {
                            SceneManager.LoadScene("Outro");
                            King.LastOne = false;
                        }
                        //King.LastOne = true;
                    }
                    
                    break;
                case "Tentatore":
                    Tempter.instance.Anim.SetBool("openFood",false);
                    break;
            }
            
            EndDialogue();
            return;
            
        }
        
        if (QuestionPosition == sentences.Count && speaker.Equals("Tentatore"))
        {
            
            Tempter.instance.Anim.SetBool("openFood",true);
            canSkip = false;
            
            var pos1 = new Vector3(158f, 43f, 0);
            var pos2 = new Vector3(1250f, 43f, 0);
            
             tmpFoodChose1 = Instantiate(foodChose1, pos1, Quaternion.identity);
             tmpFoodChose2 = Instantiate(foodChose2,pos2, Quaternion.identity);
             
             tmpFoodChose1.GetComponentInChildren<Text>().text = Tempter.instance.food1;
             tmpFoodChose2.GetComponentInChildren<Text>().text = Tempter.instance.food2;


             tmpFoodChose2.GetComponent<Button>().onClick.AddListener(WrongAnswer);
             tmpFoodChose1.GetComponent<Button>().onClick.AddListener(CorrectAnswer);
             
             var foodPos = gameObject.transform.GetChild(0).transform;
             
             tmpFoodChose1.transform.SetParent(foodPos, false);
             tmpFoodChose2.transform.SetParent(foodPos, false);
             
             Continue.gameObject.SetActive(false);
             
             Tempter.instance.done = true;
             
             RandomPosition(tmpFoodChose1.GetComponent<Button>(), tmpFoodChose2.GetComponent<Button>());
             

        }
        
        var sentence = sentences.Dequeue();

     
        

        dialogueText.text = sentence;

        Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        if (elementToSave.Count > 1)
        {
            Debug.Log("77" + elementToSave[1]);
            SaveDataInTxtFile();
        }
        
        Destroy(this.gameObject);
        Interactor.spawnedDialogue = false;
        Time.timeScale = 1;
    }

    public void  CorrectAnswer()
    {
       
    
        _audioSource.clip = buff;
        _audioSource.Play();
       
        
        
        const string affermativeSentence = "Per questa volta l'hai scampata, la mia sete non è stata sfamata ma non son un bugiardo," +
                                "\n" + " tieni questo buf ma spero che alla prossima mangerai del lardo";
        
        dialogueText.text=affermativeSentence;


        elementToSave.Insert(0, tmpFoodChose1.GetComponentInChildren<Text>().text);
        elementToSave.Insert(1,"Risposta corretta");
        
        
        
        canSkip = true;
        PlayerScript.instance.Buff();
        Destroy(tmpFoodChose1);
        Destroy(tmpFoodChose2);
        
        Continue.gameObject.SetActive(true);
        dialogueText.text = affermativeSentence;

    }

    public void  WrongAnswer()
    {
        _audioSource.clip = debuff;
        _audioSource.Play();
        canSkip = true;

        const string negativeSentence = "La tua anima si avvicina, sento la sua rinascita vicina" + "\n" +
                                "ti sembrerò un po lesto , tieni questo debuff, spero ci vedremo presto";
                
        //PlayerScript.instance.Debuff();
        
        Destroy(tmpFoodChose1);
        Destroy(tmpFoodChose2);
        
        elementToSave.Insert(0,tmpFoodChose2.GetComponentInChildren<Text>().text); 
        elementToSave.Insert(1,"Risposta errata");
       
        
        
        Continue.gameObject.SetActive(true);
        dialogueText.text = negativeSentence;


    }

    public void RandomPosition(Button food1, Button food2)
    {
        Vector3 temp;

        float randomNum = Random.Range(0f, 1f);
        
        Debug.Log("randomNum: "+randomNum);

        if (randomNum<=0.5)
        {

            temp = food1.transform.position;

            food1.transform.position = food2.transform.position;

            food2.transform.position = temp;

        }
        
    }
        
    private void SaveDataInTxtFile()
    {
        Debug.Log("77 Sono entrato SaveDataInTxtFile()");
        
        StreamWriter writer = new StreamWriter( "Assets/TxtFile/Esito.txt", true);

        for (int i = 0; i < elementToSave.Count ; i++)
        {
            
            if (i < elementToSave.Count-1)
            {
                Debug.Log("77Elemento " + i + elementToSave[i]);
                writer.Write(elementToSave[i] + "\n");
            }else 
            {
                Debug.Log("77lemento " + i + elementToSave[i]);
                writer.Write(elementToSave[i] + "\n");
                writer.Write( "--" +"\n");
                
            }

            
            
            
        }
        
        writer.Close();
        
    }
    
    
    
}
