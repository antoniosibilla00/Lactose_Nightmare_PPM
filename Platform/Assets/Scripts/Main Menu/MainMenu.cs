
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;

using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  
  public GameObject OptionMenu, ResoluctionMenu, AudioMenu, ExitMenu, CreditMenu, CommandMenu;

  public GameObject fistrInOptionMenu,
      fistrInResoluctionMenu,
      fistrInAudioMenu,
      OptionInMainMenu,
      ResoluctionInOptionMenu,
      AudioInOptionMenu,
      ExitInMainMenu,
      fistrInExitMenu,
      fistrInCreditMenu,
      CreditInMainMenu,
      firstInCommandMenu,
     
      commandInOptionMenu;

  public GameObject buttonCarica;

  private void Awake()
  {
      

    if (SaveSystem.LoadPlayer() == null)
    {
        buttonCarica.GetComponent<Button>().interactable = false;
    }
    else
    {
        buttonCarica.GetComponent<Button>().interactable = true;
    }

  }
  
  public void ExitButton(){

      Application.Quit();
      Debug.Log("Gioco Chiuso");

  }
  

  public void OpenOptionMenu(){

      OptionMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(fistrInOptionMenu);
      
  }

  public void CloseOptionMenu(){

      OptionMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(OptionInMainMenu);
      
  }

  public void OpenResoluctionMenu(){

      ResoluctionMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(fistrInResoluctionMenu);
      
  }
  
  public void CloseCommandMenu(){

      CommandMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(commandInOptionMenu);
      
  }

  public void OpenCommandMenu(){

      CommandMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(firstInCommandMenu);
      
  }


    public void CloseResoluctionMenu(){

      ResoluctionMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(ResoluctionInOptionMenu);
      
  }
  
  public void OpenAudioMenu(){

      AudioMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(fistrInAudioMenu);
      
  }

  public void CloseAudioMenu(){

      AudioMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(AudioInOptionMenu);
      
  }
    public void OpenCreditMenu(){

      CreditMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(fistrInCreditMenu);

     
  }

    public void CloseCreditMenu(){

      CreditMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(CreditInMainMenu);
      
  }

  public void OpenExitMenu(){

      ExitMenu.SetActive(true);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(fistrInExitMenu);

     
  }
  public void CloseExitMenu(){

      ExitMenu.SetActive(false);

      EventSystem.current.SetSelectedGameObject(null);
      EventSystem.current.SetSelectedGameObject(ExitInMainMenu);
      
  }

  public void DestroySaves()
    {
       
        string path = Application.persistentDataPath +"/player.fun";
        Debug.Log("ieiee"+File.Exists(path));
        if (File.Exists(path))
        {
           
            File.Delete(path);
        }
        
        
    }
}

