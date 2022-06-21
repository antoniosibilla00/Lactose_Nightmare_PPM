using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    private Transform player;
    public Animator anim;
    private Rigidbody2D body;
    private BoxCollider2D collisionBat;
    private bool attacking = false;

    private CircleCollider2D toHit;
    public BoxCollider2D playerCollider;
    private EnemiesHealthSystem healthSystem;
    private bool todo;
    public int life = 100;
    public enum State     {normal,death }
    public State state;

    private HealthSystem takeDamage;
    
   


    private void Start()
    {
        todo = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
        toHit = GetComponentInChildren<CircleCollider2D>();
        anim= GetComponent<Animator>();
        collisionBat = gameObject.GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(playerCollider, collisionBat );

        healthSystem = this.GetComponent<EnemiesHealthSystem>();
        anim.SetBool("die", false );
        
        

    }


    private void Update()
    {
  
        switch (state)
        {
            case State.normal:
                
                pathFinding();
                Flip();
                if (healthSystem.GetCurrentHealth()<=0)
                {
                    anim.SetTrigger("die");
                    state = State.death;
                }

                break;
            case State.death :

                this.GetComponent<CapsuleCollider2D>().enabled= false;

            
                break;
        }
        
        
    }
 
        
    

    private void Flip(){
        
            if(transform.position.x > player.transform.position.x ){

                transform.rotation = Quaternion.Euler(0,180,0);

            }else{

                transform.rotation = Quaternion.Euler(0,0,0);
                
            }
            
    }

 
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }

    private void pathFinding( )
    {

            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange){

                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);

            }else if (distanceFromPlayer <= shootingRange )
            {
                anim.SetTrigger("attack");
                
                
                
            }
        
    }

  

    public void ItsTimeToDestroy()
    {
        Destroy(gameObject);
       
    }

    public void ImAttacking()
    {

        toHit.enabled = true ;
        

    }
    public void ImNotAttacking()
    {

        toHit.enabled = false;
    }
}

