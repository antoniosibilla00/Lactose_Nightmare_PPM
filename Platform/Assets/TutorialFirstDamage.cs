using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFirstDamage : MonoBehaviour
{
    private bool done;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        done = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !done)
        {
            
            HealthSystem.Instance.TakeDamage(10);
            done = true;

        }
    }
}
