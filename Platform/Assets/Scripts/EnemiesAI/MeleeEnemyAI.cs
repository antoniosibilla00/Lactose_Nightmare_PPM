using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using Pathfinding;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class MeleeEnemyAI
    : MonoBehaviour
{
    [Header("Pathfinding")]
    private Transform target;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed ;
    public float nextWaypointDistance = 3f;
    
    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool directionLookEnabled = true;
    public bool canwalk = true;

    private Path path;
    private int currentWaypoint;

    #region AI variables
    private GameObject Alexander;
    [SerializeField]private float activateDistance;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform range;
    [SerializeField] private float rangeRadius;
    [SerializeField] private LayerMask player;
    private BoxCollider2D playerBoxCollider2D; 
    private CapsuleCollider2D playerCapsuleCollider2D;
    private BoxCollider2D meleeEnemyCollider;
    private Rigidbody2D playerBody;
    public LayerMask enemiesLayer;
    [SerializeField]private float timer;
    #endregion

    #region Animator variables
    private bool attack ;
    private bool takeHit;
    private bool dead;
    private bool walk;
    bool facingRight;
    [SerializeField]public Vector2 force;
    private bool cooldown;
    private float actualTimer;
    private bool isNotPlaying;
    #endregion


    public AudioSource AudioSource;
    public AudioClip enemyDies;
    public AudioClip soundAttack;
    private EnemiesHealthSystem healthSystem;
    [SerializeField] public Collider2D attackHitBox;


    
    public enum State
    { 
        normal,
        attacking,
        death,
        idle
    }
    
    public State state;
    


    public void Awake()
    {
        dead = false;
        
        
        AudioSource = GetComponent<AudioSource>();

    }

    public void Start()
    {
        Alexander = GameObject.FindWithTag("Player");
        rb = Alexander.GetComponent<Rigidbody2D>();
        playerBoxCollider2D = Alexander.GetComponent<BoxCollider2D>();
        playerCapsuleCollider2D = Alexander.GetComponent<CapsuleCollider2D>();
        target = Alexander.GetComponent<Transform>();
        meleeEnemyCollider = GetComponentInChildren<BoxCollider2D>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        Physics2D.IgnoreCollision(playerBoxCollider2D,meleeEnemyCollider);
        Physics2D.IgnoreCollision(playerCapsuleCollider2D,meleeEnemyCollider);
        healthSystem = GetComponentInChildren<EnemiesHealthSystem>();
        attack = false;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        
        Debug.Log("target"+target);
     
        Debug.Log("attack"+attack);
        Debug.Log("walk"+walk);
        Debug.Log("cooldown"+cooldown);
        
        anim.SetBool("Cooldown",cooldown);
        anim.SetBool("attack",attack);
        if (canwalk)
        {
            anim.SetBool("Walk",walk);
        }
      
     
        isNotPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName("dead") == false;
        
        if (healthSystem.GetCurrentHealth() <= 0 &&!dead && isNotPlaying)
        {
            
            anim.SetTrigger("dead");
            this.GetComponentInChildren<CapsuleCollider2D>().enabled= false;
            if (!AudioSource.isPlaying)
            {
                AudioSource.clip = enemyDies;
                AudioSource.Play();
            }
            
            
            state = State.death;
        }

        #region Manage Enemy states (In terms of Animation)
        switch (state)
        {

            case State.death :

               
               
                if (dead)
                {
                    Destroy(transform.parent.gameObject);
                    Destroy(gameObject);
                } 
                break;
            
            
            case State.idle:
              
                walk = false;
               
                break;
                
        }
        #endregion

    }

    private void FixedUpdate()
    {
        
     
       
        #region Manage Enemy states (Physics)
        
        switch (state)
        { 
            case State.normal:
                
                    attackHitBox.enabled = false;

                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") == false ) 
                    {
                    
                        takeHit = false;
                                        
                        Collider2D enterInRange = Physics2D.OverlapCircle(range.position, rangeRadius, player);
                    
                        if (enterInRange!=null && cooldown == false && healthSystem.GetCurrentHealth()>0)
                        {
                            actualTimer = timer;
                        
                        
                            state = State.attacking;

                            attack = true;
                            if (target.position.x > transform.position.x)
                            { 
                                facingRight = false;
                                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                            }else{
                                facingRight = true;
                                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

                            }
                     

                        }
                    
                        else if(BecameInactive())
                        {
                            state = State.idle;
                        
                        }
                    
                        else
                        {
                            if (TargetInDistance() && followEnabled)
                            {
                                walk = true;
                                PathFollow();
                            }
                        }
                    }

                    break;
            
            case State.attacking:
                actualTimer -= Time.deltaTime;
                if (actualTimer <= 0 && attack == false)
                {
                    actualTimer = timer;
                    cooldown = false;
                    state = State.normal;
                }
                break;
         
            case State.idle:
               
                if (TargetInDistance())
                {
                    cooldown = false;
                    state = State.normal;

                }
                break;
        }

    }
    #endregion
    
    private void UpdatePath()
    {
        if (!attack)
        {

            if (followEnabled && TargetInDistance()&& seeker.IsDone())
            {
            
                seeker.StartPath(rb.position, target.position, OnPathComplete);
            }
        }
       
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
           
            return;
        }
        
        
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; 
        force = direction * speed * Time.deltaTime;
        
        // Movement
      
            rb.AddForce(force, ForceMode2D.Impulse);

        if (rb.velocity.x> speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (rb.velocity.x <speed*(-1))
        {
            rb.velocity = new Vector2(speed*(-1), rb.velocity.y);
        }
      
        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f && facingRight)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                facingRight = false;
               
            }
            else if (rb.velocity.x < -0.05f && !facingRight)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                facingRight = true;
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }
    
    private bool BecameInactive()
    {
        return Vector2.Distance(transform.position, target.transform.position) > activateDistance;
    }
    

    private void OnPathComplete(Path p)
    {
        
        if (!p.error)
        {
            
            path = p;
            currentWaypoint = 0;
        }
    }

    void startAnim()
    {
        attackHitBox.enabled = true;
        /*
        AudioSource.clip = soundAttack;
        AudioSource.Play();
        */
    }
    void finishAnim()
    {
        attack = false;
        cooldown = true;
        walk = false;
        Debug.Log("finishAnim");
        
        attackHitBox.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(range.position,rangeRadius);
    }
    
    void isDead()
    {
    
        dead = true;
    }

    public void SetSpeed(float temp)
    {
        speed = temp;
    }
    
    
    public float GetSpeed()
    {
        return speed ;
    }
    
    
    
}