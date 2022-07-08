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
    private GameObject Alexander;
    
    private bool attacking = false;

    private CircleCollider2D toHit;
    private BoxCollider2D playerBoxCollider2D;
    private CapsuleCollider2D playerCapsuleCollider2D;
    private EnemiesHealthSystem healthSystem;
    
    private bool todo;
    [SerializeField]private float timer;
    private bool cooldown;
    private float actualTimer;
    private bool isNotPlaying;
    private bool dead;
    private bool attack;
    private bool canDie ;
    public enum State     {normal,death,attacking }
    public State state;
    


    public void Awake()
    {
        Alexander = GameObject.FindGameObjectWithTag("Player");
        playerBoxCollider2D = Alexander.GetComponent<BoxCollider2D>();
        playerCapsuleCollider2D = Alexander.GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        todo = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        collisionBat = gameObject.GetComponentInChildren<BoxCollider2D>();
        toHit = GetComponentInChildren<CircleCollider2D>();
        Physics2D.IgnoreCollision(playerBoxCollider2D, collisionBat ,true);
        Physics2D.IgnoreCollision(playerCapsuleCollider2D, collisionBat ,true);
        dead = false;
        healthSystem = this.GetComponentInChildren<EnemiesHealthSystem>();
        canDie = false;



    }


    private void Update()
    {
        anim.SetBool("cooldown",cooldown);
        isNotPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName("dead") == false;
        
        if (healthSystem.GetCurrentHealth()<=0 && !dead && isNotPlaying)
        {
            anim.SetTrigger("die");
            state = State.death;
        }

        switch (state)
        {
            case State.normal:
                
                pathFinding();
                Flip();
              
                break;
            case State.death :
                this.GetComponentInChildren<CapsuleCollider2D>().enabled= false;
                if (dead)
                {
                    Destroy(gameObject,2f);
                }
                
                break;
            
            case State.attacking:
                
                actualTimer -= Time.deltaTime;
                
                if (actualTimer<=0)
                {
                    cooldown = false;
                    state = State.normal;
                }
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

            if (cooldown) return;

            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange ){

                transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);

            }else if (distanceFromPlayer <= shootingRange)
            {
                anim.SetTrigger("attack");
                actualTimer = timer;
                state = State.attacking;
                
            }

    }
    

    public void ImAttacking()
    {

        toHit.enabled = true ;
        

    }
    public void ImNotAttacking()
    {
        cooldown = true;
        toHit.enabled = false;
    }

    public void BecameDead()
    {
        dead = true;
    }

    public float GetSpeed()
    {
        return speed;
    }
    
    public void SetSpeed(float newSpeed)
    { 
        speed=newSpeed;
    }

}

