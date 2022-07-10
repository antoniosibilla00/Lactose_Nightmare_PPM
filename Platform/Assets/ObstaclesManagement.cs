using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclesManagement : MonoBehaviour
{
    private bool done=false;// Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") &&!done)
        {
            done = true;
            HealthSystem.Instance.TakeDamage(100);
        }
    }
}
