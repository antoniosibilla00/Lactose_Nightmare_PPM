using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ComandiPauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject ButtonFistrSelection;
    
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(ButtonFistrSelection);
    }
    
}
