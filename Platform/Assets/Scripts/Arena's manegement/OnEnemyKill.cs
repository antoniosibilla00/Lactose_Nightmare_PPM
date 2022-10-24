using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Arena2;

public class OnEnemyKill : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log("Alexandra.counter"+ ArenaManagement.enemiesCounter);
        ArenaManagement.enemiesCounter--;
        Debug.Log("newAlexandra.counter"+ ArenaManagement.enemiesCounter);
    }
}
