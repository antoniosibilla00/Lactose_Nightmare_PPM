using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class bulletScript : MonoBehaviour
{
    private GameObject target;
    public float speed;
    private Rigidbody2D bulletRB;
    
    
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"),LayerMask.NameToLayer("Background"));
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed *(this.GetComponent<Transform>().localScale.x);

        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
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
