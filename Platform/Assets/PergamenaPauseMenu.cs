using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Button = UnityEngine.UIElements.Button;

public class PergamenaPauseMenu : MonoBehaviour
{
    static int nCollectedScrolls ;
    
     private static readonly string[] scrolls = { 
        "Vi sarà narrato di una condizione che affligge molti nel nostro tempo.\n Eravamo alla ricerca di rimedi magici per curare molte malattie sconosciute e una di queste era rappresentata dall'intolleranza al lattosio.\n" +
        "\n Non capivamo cosa potesse indurre una cattiva digestione del latte e si pensava fosse una condizione indotta da una sorta di maledizione.\n"+
        "\nUn giorno però siamo riusciti a capire cosa non andava, le persone intolleranti hanno una carenza di un enzima digestivo chiamato lattasi.\n"+
        "\nMa quello che abbiamo scoperto non finisce qui infatti...",
        
        "…Infatti, l'intolleranza al lattosio può essere causata da un maleficio ereditario che si manifesta nella vita di una persona sin dalla nascita, rendendola incapace di digerire il latte e i suoi derivati per tutta la vita.\n" +
        "\nTuttavia, alcuni vecchi scritti alchemici  suggeriscono che l'intolleranza al lattosio può anche essere il risultato di un'infezione temporanea, che può portare a sintomi come nausea, diarrea e dolori addominali.\n"+
        "\nE ancora ….",
        
        "...E ancora, sempre secondo gli antichi scritti alchemici, la malvagia condizione dell'intolleranza al lattosio può attaccare anche i neonati e i bambini, sebbene in rari casi.\n "+
        "\nSi dice che questa maledizione sia causata dall'influenza delle stelle o degli spiriti maligni, e che colpisca il corpo fin dalla nascita.\n",
        
        "Ascoltate, poiché vi porterò alla conoscenza di un segreto rivelato a pochi .\n "+
        "\nSi dice che il latte materno contenga circa il doppio della quantità di lattosio rispetto al latte animale.\n"+
        "\nComunque ricordatevi non confondere l'intolleranza al lattosio con l'allergia al latte, che è una reazione magica del sistema immunitario alle proteine del latte.\n\nIn questo caso, il corpo identifica le proteine del latte come pericolose e le attacca, causando sintomi più gravi come eruzioni cutanee, difficoltà respiratorie e shock anafilattico",

        "Nonostante ciò, abbiamo capito che alcuni individui credono di essere intolleranti al lattosio anche se, in realtà, non lo sono.\n"+
        "\nSi dice che queste persone siano state influenzate da fattori magici o misteriosi, che hanno distorto la loro percezione della realtà.\n"+ 
        "\nOra sappiamo che non è altro che il risultato di una dieta squilibrata o di uno stile di vita disordinato, il quale provoca sintomi simili all’intolleranza al lattosio.\n"+
        "\nAnche perchè le persone non sanno che…",
        
        "…Anche perché le persone non sanno che recentemente abbiamo scoperto un altro maleficio, la celiachia.\n"+
        "\nQuest’ ultimo risulta essere ben peggiore dell’intolleranza al lattosio, in quanto se non curata può portare a gravi danni all'intestino tenue e non solo.\n"+
        "\nEd  molto comune nei soggetti celiaci che questi siano anche intolleranti al lattosio, in quanto la celiachia può portare a carenza di lattasi",
        
        "Continuo a riportarvi le nostre conoscenze inerenti all'intolleranza al lattosio, poiché credo che possano essere di grande interesse per voi. Si dice che non esiste alcuna elisir, magia o medicinale in grado di invertire il disturbo pertanto bisogna limitare la quantità di lattosio ingerita attraverso la propria dieta.\n"+
        "\nQuesto però può causare una carenza di calcio nel corpo ma, non disperate, o saggio popolo, perché ci sono molte alternative nutrizionali che possono aiutarvi a compensare la mancanza di calcio nella vostra dieta come ad es pesce e verdure\n" +
        "\nNonostante ciò…",

        "Nonostante ciò abbiamo appreso che esistono elisir magici e medicinali che possono aiutare i soggetti intolleranti al lattosio a consumare latte e prodotti lattiero-caseari senza ripercussioni negative.\n"+
        "\nQuesti rimedi sono considerati delle autentiche pietre filosofali, poiché sono in grado di trasmutare l'intolleranza al lattosio in una tolleranza accettabile.\n"+
        "\nTuttavia, è importante sottolineare che questi elisir non sono facilmente reperibili e il loro utilizzo deve essere prescritto dal proprio medico o alchimista di fiducia.\nMa non temete...",
        
        "Ma non temete, o popolo, poiché esistono molti sostituti del latte che possono soddisfare le vostre esigenze nutrizionali, come ad esempio il latte di riso, di avena, di mandorle, di soia, di cocco e di nocciole\n"+
        "\nE ricordate sempre che la saggezza alchemica ci insegna che il cibo è un'arma preziosa per la nostra salute e il nostro benessere\n"+
        "\nDa gli alchimisti è tutto.\n\nCercheremo di approfondire ancora e ancora per avere una visione chiara di questo maleficio, sperando che questo possa questa conoscenza portare guarigione e benessere a coloro che ne hanno bisogno.",

        
    };

    public int[] collectedScrolls;
    
    [SerializeField] private GameObject pergamenaButton;
    
    [SerializeField] private PauseMenu pauseMenu;
    
    [SerializeField] private GameObject ButtonBack;
    
    [SerializeField] private GameObject ButtonNext;
    [SerializeField] private GameObject ButtonBefore;

    private ArrayList text = new ArrayList();
    

    private int index = 0;

    private void OnEnable()
    {
        text.Clear();
        
        EventSystem.current.SetSelectedGameObject(ButtonBack);
        index = 0;

        for (int i = 0; i < PlayerScript.instance.indexSrollsCollected.Count; i++)
        {
            
            text.Add(scrolls[PlayerScript.instance.indexSrollsCollected[i]]);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (text.Count > 0)
        {
            
            this.gameObject.GetComponentInChildren<Text>().text = (string)text[index];
            
            Debug.Log("//// iundex " + index );
            Debug.Log("//// i text.count  " + text.Count );
            

            if (index < text.Count && index != 0 && index != text.Count-1 ) //In mezzo
            {
                
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                    changeTextNext();

                }else if(Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    changeTextBefore();

                }
                
                
                ButtonBefore.SetActive(true);
                ButtonNext.SetActive(true);
                
            }
            else if(index == text.Count-1 && index != 0 ) //Ultimo
            {
                
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {

                    changeTextBefore();

                }
                
                ButtonBefore.SetActive(true);
                ButtonNext.SetActive(false);
                
            }else if (index == 0 && text.Count <= 1) //Primo quando abbiamo raccolto solo una pergamena
            {
                
                ButtonBefore.SetActive(false);
                ButtonNext.SetActive(false);
                
                
            }else if (index == 0 && text.Count > 1) //Primo quando abbiamo raccolto più pergamene
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                    changeTextNext();

                }
                
                
                ButtonBefore.SetActive(false);
                ButtonNext.SetActive(true);
                
                
            }
           
        }
        
    }

    public void addIndexToList(int index)
    {
        pauseMenu.addPergamena();
        
       
        
    }
    
    public void changeTextNext()
    {

        ++index;

    }
    
    public void changeTextBefore()
    {

        --index;

    }

    public static int getNPergamene()
    {

        return nCollectedScrolls;

    }
    
}
