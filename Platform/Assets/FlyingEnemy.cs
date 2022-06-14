using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{

    public float speed;
    public float lineOfSite;
    public float shootingRange;
    private Transform player;
    public Animator anim;
    public bool iWillFindU = true; 
    public bool notStattFerm = false; 
    private Rigidbody2D body;
    private BoxCollider2D collision;
    public LayerMask groundLayer ;

    
    public float life = 10;


    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = GetComponent<Rigidbody2D>(); 
        anim= GetComponent<Animator>();
        collision = gameObject.GetComponent<BoxCollider2D>();

        collision.enabled = false;

    }


    private void Update()
    {

        getDamage();

        pathFinding(iWillFindU);
        
        Flip(iWillFindU);

        Debug.Log(IsGrounded());
        
        if (IsGrounded() && notStattFerm)
        {
            body.bodyType = RigidbodyType2D.Kinematic;
            collision.enabled = false;
                
        }

       
    }

    private void Flip(bool todo){

        if(todo){

            if(transform.position.x > player.transform.position.x ){

                transform.rotation = Quaternion.Euler(0,180,0);

            }else{

                transform.rotation = Quaternion.Euler(0,0,0);


            }
        }
    }

    private void getDamage(){

        if(Input.GetKeyDown(KeyCode.E) && life > 1){

            life -= 1;
            Debug.Log(life);

        }else if (Input.GetKeyDown(KeyCode.E) && life == 1){
            notStattFerm = true;
            life -= 1;
            body.bodyType = RigidbodyType2D.Dynamic;
            collision.enabled = true;
            anim.SetTrigger("die");
           
            iWillFindU=false;

          
            
            
        }

    }

    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);

    }

    private void pathFinding(bool todo)
    {

        if(todo){
        
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange){

                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed*Time.deltaTime);

            }else if (distanceFromPlayer <= shootingRange ){

                anim.SetTrigger("attack");

            }
        }

    }

    public bool IsGrounded(){ 

            RaycastHit2D raycast = Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0, Vector2.down, 0.1f, groundLayer);         
            return raycast.collider != null;     
        
    }


}

