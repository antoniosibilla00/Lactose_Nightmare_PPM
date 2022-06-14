using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Queue<string>sentences;
    [SerializeField]private  Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private GameObject DialogueBox;
    private void Start()
    {
        sentences = new Queue<string>();
        
    }

    public void StartDialogue(Dialogue dialogue)
    { 
        dialoguePanel.SetActive(true); 
        sentences.Clear();

        nameText.text = dialogue.name;

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
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        dialogueText.text = sentence;
            
        Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    
}
