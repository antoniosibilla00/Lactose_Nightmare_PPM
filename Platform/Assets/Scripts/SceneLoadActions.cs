using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadActions : MonoBehaviour
{
    [SerializeField] private Transform player; //drag player reference onto here
    [SerializeField] private Vector3 _targetPosition; //here you store the position you want to teleport your player to
    public GameObject[] cavesParallax;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    //After adding this method to the delegate, this method will be called every time
    //that a new scene is loaded. You can then compare the scene loaded to your desired
    //scenes and do actions according to the scene loaded.
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
      
      
    }


    private void Start()
    {
         Debug.Log("scene.setParallax"+GameObject.FindGameObjectWithTag("Player").transform.position.y);
            if (GameObject.FindGameObjectWithTag("Player").transform.position.y < 17.485f)
            {
                Debug.Log("scene.setParallax");
                SetParallax();
            }
    }


    private void SetParallax()
    {
        foreach (var caveParallax in cavesParallax)
        {
            caveParallax.SetActive(true);
        }

    }
}