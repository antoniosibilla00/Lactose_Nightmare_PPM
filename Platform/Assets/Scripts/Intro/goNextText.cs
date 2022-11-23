using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class goNextText : MonoBehaviour
{
    
    string[] sentences = {"Non molto tempo fa, in una città provinciale, viveva il giovane Alexander.",
        "Nel suo gruppo di amici è sempre stato conosciuto per adorare il cibo contenente lattosio."+"\n"+"Ne ha sempre mangiato in grande quantita'.",
        "Un giorno tuttavia, di ritorno da una festa, si sentì improvvisamente male."+"\n"+"Una fitta lancinante allo stomaco cominciò a tormentarlo, tanto che nessuna medicina gli fece effetto."+"\n"+"La madre, molto preoccupata, lo portò dal medico di famiglia, il dottor Antoine.", 
        "Fu con somma tristezza che ricevette un tragico referto medico: col tempo il suo organismo era diventato intollerante al lattosio!"+"\n"+"\n"+"Tornando a casa, comincio' a pensare a tutti i cibi che non avrebbe più potuto mangiare e varcata la porta d'ingresso si gettò sul divano."+"\n"+"\n"+"L'unica cosa che riuscì a non fargli pensare all'accaduto fu l'annuncio di un nuovo videogioco in cui si deve salvare un regno in pericolo: The King's Nightmare.",
        "Pensando alle avventure che avrebbe vissuto nel nuovo videogame riuscì ad addormentarsi, tuttavia il sonno non fu piacevole....."
    };

    [SerializeField] public Text sentenceShow;
    public bool change;
   
    private int nSentence ;
    private int actualSentence = 0;
    
    [SerializeField] private Button buttonSkip;
    [SerializeField] private Button buttonLoad;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        sentenceShow.text =  sentences[actualSentence];
        nSentence = sentences.Length;
        
        Debug.Log(">>>>>nSentence" +  nSentence);
    }

    // Update is called once per frame
    void Update()
    {
        
        


        
        
        if (change)
        {
            Debug.Log(">>>>>HoCambiatoSentence");
            sentenceShow.text =  sentences[actualSentence]; 
            change = false;
            
        }
        
        

    }


    public void TextChange()
    {
        Debug.Log(">>>>>HoCambiato");
        Debug.Log(">>>>>HoCambiatonSentence" + (nSentence-1));
        Debug.Log(">>>>>actualSentence" + actualSentence);
        if ((nSentence-1) > actualSentence+1 )
        {
            
            actualSentence += 1;
            change = true;
            Debug.Log(">>>>>actualSentence2  " + actualSentence);
            
        }
        else
        {
            actualSentence += 1;
            change = true;
            
            buttonSkip.gameObject.SetActive(false);
            buttonLoad.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(buttonLoad.gameObject);
            
            Debug.Log(">>>>>finito");
            
            

        }

        
        
        

    }

}
