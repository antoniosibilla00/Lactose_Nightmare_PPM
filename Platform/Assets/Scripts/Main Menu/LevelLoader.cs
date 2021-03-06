using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class LevelLoader : MonoBehaviour
{
    string[] insights = {"Il 90% dei nord Europei mantiene l’abilità di digerire il lattosio dopo l’infanzia contro il 5% degli Asiatici",
        "Alcuni soggetti intolleranti accusano irritabilità e sbalzi d’umore", 
        "Il valore soglia dell’intolleranza varia da persona a persona. In alcuni casi i sintomi sono talmente lievi da essere ignorati", 
        "La probabilità di essere intolleranti sale per quella parte della popolazione la cui dieta normalmente esclude i latticini"};

    
    public static LevelLoader instance;

    [SerializeField] public Text insightsText1;
    [SerializeField] public GameObject player;

    [SerializeField] private Canvas loaderCanvas;
    [SerializeField] private Slider progressBar;

    private float target;

    private void Awake()
    {
        Debug.Log(instance);
        if(instance == null){

            instance = this;
            
            DontDestroyOnLoad(gameObject);


        }else{

            Destroy(gameObject);

        }

    }

    public async void LoadScene(string sceneName)
    {
        target = 0;
        progressBar.value = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false ;

        loaderCanvas.gameObject.SetActive(true);
          
        insightsText1.text =  insights[Random.Range(0, 4)]; 
        
        do{

            await Task.Delay(2000);
            target = scene.progress;
            Debug.Log("target"+target);
            

        }while(scene.progress < 0.9f);

        await Task.Delay(3500);

        scene.allowSceneActivation = true ;

        
        loaderCanvas.gameObject.SetActive(false);

    }
    
    
    public async void LoadScene(int  sceneName)
    {
        target = 0;
        progressBar.value = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false ;

        loaderCanvas.gameObject.SetActive(true);
          
        insightsText1.text =  insights[Random.Range(0, 4)]; 
        
        do{

            await Task.Delay(2000);
            target = scene.progress;
            Debug.Log("target"+target);
            

        }while(scene.progress < 0.9f);

        await Task.Delay(3500);

        scene.allowSceneActivation = true ;

        
        loaderCanvas.gameObject.SetActive(false);

    }

    void Update()
    {
        Debug.Log(progressBar);
    
        
        progressBar.value = Mathf.MoveTowards(progressBar.value,target,3*Time.deltaTime);

     

    }



}