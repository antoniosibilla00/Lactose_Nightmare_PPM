using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class bulletScript : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;
    public Transform witchTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir =( (target.transform.position - transform.position).normalized * speed *(this.GetComponent<Transform>().localScale.x))/*   *gameObject.transform.parent.gameObject.GetComponentInParent<Transform>().localScale.z*/;
        Debug.Log("localScale.witch: "+witchTransform.localScale.x);
        if (witchTransform.localScale.x>0)
        {
            bulletRB.velocity = new Vector2(-1* Mathf.Abs(moveDir.x) , moveDir.y);
        }
        else
        {
            bulletRB.velocity = new Vector2(1* Mathf.Abs(moveDir.x) , moveDir.y);
        }
       
        Debug.Log("neWvettoreProiettile.witch: "+bulletRB.velocity);
        Debug.Log("vettoreProiettile.witch: "+bulletRB.velocity.x*transform.localScale.x+" "+bulletRB.velocity.y);
        
        
        
        Destroy(this.gameObject, 2);

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            FindObjectOfType<HealthSystem>().TakeDamage(10);
            Destroy(gameObject);
        }
        else if (col.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}
