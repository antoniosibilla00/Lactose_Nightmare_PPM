using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocolateFight : MonoBehaviour
{

    public float speed;
    public float circileRadiusGround;
    public float circileRadiusWall;
    public float lineOfSite;
    public float shootingRange;
    private Rigidbody2D EnemyRB;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject groundCheck;
    public GameObject wallCheck;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    public LayerMask groundLayer;
    public bool facingRight;
    public bool isGrounded;
    public bool isWall;
    public Animator anim;
    public bool iWillFindU = false;
    public bool run;
    public bool attack = false;
    private float x;
    
    private bool iNeeedToStayThere = false;
    
    private Vector3 positionEnemy;


    void Start()
    {

        EnemyRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim= GetComponent<Animator>();

    }

    private void Update()
    {
        EnemyRB.velocity = Vector2.right * speed * Time.deltaTime;
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, circileRadiusGround, groundLayer);
        isWall = Physics2D.OverlapCircle(wallCheck.transform.position, circileRadiusWall, groundLayer);

        move(facingRight,run );
        pathFinding(true);

        whatIDo(iWillFindU);






    }

    public void whatIDo(bool iWillPrendU)
    {
        
        switch (iWillPrendU)
        {
            
            case true : FlipWhenUFindPlayer(iWillFindU);
                break;
            case false : if (isGrounded && facingRight && isWall || isGrounded && !facingRight && isWall)
                {
                    flip();
                }
                break;
            
        }


        
    }

    // Update is called once per frame
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, circileRadiusGround);
        Gizmos.DrawWireSphere(wallCheck.transform.position, circileRadiusWall);
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
     
 
    }


    private void flip()
    {
        
        facingRight = !facingRight;
        transform.Rotate(new Vector3(0,180,0));

    }

    private void FlipWhenUFindPlayer(bool todo){

        if(todo){

            if(transform.position.x > player.transform.position.x ){

                facingRight = false;
                transform.rotation = Quaternion.Euler(0,180,0);

            }else{
                facingRight = true;
                transform.rotation = Quaternion.Euler(0,0,0);


            }
        }
    }
    
    private void move(bool direction, bool todo)
    {
        if (todo)
        {
            anim.SetTrigger("walk");
            switch (direction)
            {
                case true : transform.position += new Vector3(0.001f, 0.0f, 0.0f);break;
            
                case false: transform.position -= new Vector3(0.001f, 0.0f, 0.0f);break;
                    
            }
            
            
        }
        
        
    }
    
    
    private void  pathFinding(bool todo)
    {

        if(todo && isGrounded){
        
            float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

            if(distanceFromPlayer < lineOfSite && distanceFromPlayer > shootingRange){

                if (!attack)
                {
                    run = true;
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, 0*Time.deltaTime);
                    iWillFindU = true;
                    
                }
                else
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, player.position, 0*Time.deltaTime);
                    iWillFindU = true;
                    run = false;
                    attack = false;
                }
                   
                   
                
                
            }else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
                run = false;
                attack = true;
                anim.SetTrigger("attack");

            }else if (distanceFromPlayer > lineOfSite)
            {
                attack = false;
                iWillFindU = false;
                run = true;
                
            }
            
            
        }

      
    }

}
