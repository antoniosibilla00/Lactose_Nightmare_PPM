using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private int nPergameneRaccolte = 0 ;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject alexanderUI;

    [SerializeField] private GameObject buttonFistrSelected;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pergamenaButton;

    private void OnEnable()
    {
        Debug.Log("//// on enable");
        EventSystem.current.SetSelectedGameObject(buttonFistrSelected);

    }

    // Update is called once per frame
    void Update()
    {

        if (menu.activeInHierarchy)
        {
            Debug.Log("//// menu count " + PlayerScript.instance.indexSrollsCollected.Count);
            
            if (PlayerScript.instance.indexSrollsCollected.Count <= 0)
            {
                pergamenaButton.GetComponent<Button>().interactable = false;

                pergamenaButton.GetComponentInChildren<Text>().color = Color.grey;

            }else
            {
                pergamenaButton.GetComponent<Button>().interactable = true;
            
                pergamenaButton.GetComponentInChildren<Text>().color = Color.white;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
     
        
    }
    
 
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        alexanderUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        alexanderUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        
        MusicManager.istance.PlayMenuOst();
        
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        
       Application.Quit();
        
    }

    public void addPergamena()
    {
        
        ++nPergameneRaccolte;

    }

    public void setButtonSelected()
    {
        
        EventSystem.current.SetSelectedGameObject(buttonFistrSelected);
        
    }

}


