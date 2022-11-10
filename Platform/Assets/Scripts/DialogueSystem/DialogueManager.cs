using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
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
            
            var pos1 = new Vector3(1374.74f, 70.87698f, 0);
            var pos2 = new Vector3(580.3571f, 70.87698f, 0);
            
             tmpFoodChose1 = Instantiate(foodChose1, pos1, Quaternion.identity);
             tmpFoodChose2 = Instantiate(foodChose2,pos2, Quaternion.identity);
             
             tmpFoodChose1.GetComponentInChildren<Text>().text = Tempter.instance.food1;
             tmpFoodChose2.GetComponentInChildren<Text>().text = Tempter.instance.food2;


             tmpFoodChose2.GetComponent<Button>().onClick.AddListener(WrongAnswer);
             tmpFoodChose1.GetComponent<Button>().onClick.AddListener(CorrectAnswer);
             
             var foodPos = gameObject.transform.GetChild(0).transform;
             
             tmpFoodChose1.transform.SetParent(foodPos);
             tmpFoodChose2.transform.SetParent(foodPos);
             
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
        Destroy(this.gameObject);
        Interactor.spawnedDialogue = false;
    }

    public void  CorrectAnswer()
    {
       
    
        _audioSource.clip = buff;
        _audioSource.Play();
       
        
        
        const string sentence = "Per questa volta l'hai scampata, la mia sete non è stata sfamata ma non son un bugiardo," +
                                "\n" + " tieni questo buf ma spero che alla prossima mangerai del lardo";
        
        dialogueText.text=sentence;
        
        canSkip = true;
        PlayerScript.instance.Buff();
        Destroy(tmpFoodChose1);
        Destroy(tmpFoodChose2);
        
        Continue.gameObject.SetActive(true);
        dialogueText.text = sentence;

    }

    public void  WrongAnswer()
    {
        _audioSource.clip = debuff;
        _audioSource.Play();
        canSkip = true;

        const string sentence = "La tua anima si avvicina, sento la sua rinascita vicina" + "2n" +
                                "ti sembrerò un po lesto , tieni questo debuff, spero ci vedremo presto";
                
        PlayerScript.instance.Debuff();
        
        Destroy(tmpFoodChose1);
        Destroy(tmpFoodChose2);
        
        Continue.gameObject.SetActive(true);
        dialogueText.text = sentence;


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
        
    
}
