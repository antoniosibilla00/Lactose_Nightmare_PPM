using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBite : MonoBehaviour
{
    private CircleCollider2D bite;
    // Start is called before the first frame update
    void Start()
    {
        bite = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Antonio è dentro ");
        if (col.CompareTag("Player"))
        {
            
            col.GetComponent<HealthSystem>().TakeDamage(10);
            
        }
    }
    
    public void ImAttacking()
    {

        bite.enabled = true ;
        

    }
    public void ImNotAttacking()
    {

        bite.enabled = false;
    }
}
