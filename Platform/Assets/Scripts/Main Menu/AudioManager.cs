using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

public static AudioManager instance;

void Awake(){

    if(instance == null){

        instance = this;

        DontDestroyOnLoad(gameObject);

    }else{

        Destroy(gameObject);

    }

}

private void Start()
{
    /*
    var audioSource = GetComponent<AudioSource>();
    audioSource.Play();
    */
}
}
