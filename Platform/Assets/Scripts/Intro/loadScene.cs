using UnityEngine.SceneManagement;
using UnityEngine;


public class loadScene : MonoBehaviour
{
 
        
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
