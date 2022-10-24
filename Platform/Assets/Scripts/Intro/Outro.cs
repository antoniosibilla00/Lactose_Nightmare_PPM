using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Outro : MonoBehaviour
{
    string[] sentences = {"Al risveglio Alexander si sentì più forte e capace di affrontare una vita priva di prodotti che contengono lattosio.",
        "Dopotutto lui in cuor suo è Sir Alexander, L'eroe di Lactoseville !...",
        "Forte di questa consapevolezza visse felice e contento.",
        "FINE"
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
            
            Debug.Log(">>>>>finito");
            
            

        }

        
        
        

    }

}
