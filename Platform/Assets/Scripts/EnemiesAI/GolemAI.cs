using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;

public class GolemAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform range;
    [SerializeField] private float rangeRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private Collider2D player2;
    [SerializeField] private Collider2D Golem;
     public int golemHealth;
    [SerializeField] private Rigidbody2D playerBody;
    private Vector2 currentVelocity = new Vector2(2,0);
    private bool attack ;
    private bool takeHit;
    private bool dead;
    private bool walk;
    private Vector2 force;
    private bool cooldown;
    private float timer;
    private float actualTimer;
    public enum State
    {
        normal,
        attacking,
        death,
       
    }
    
    public State state;
    public GolemAI instance;


    public void Awake()
    {
        instance = this;
        golemHealth = 100;
        dead = false;
        timer = 2f; 
    }

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        Physics2D.IgnoreCollision(player2,Golem);
        attack = false;
        
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        
        anim.SetBool("attack",attack);
        anim.SetBool("walk",walk);
        anim.SetBool("cooldown",cooldown);
        
        
        switch (state)
        {
            case State.normal:
                if (golemHealth<0)
                {
                    anim.SetTrigger("dead");
                    state = State.death;
                }
                
                break;
            case State.death :

                this.GetComponent<CapsuleCollider2D>().enabled= false;
               
                if (dead)
                {
                    Destroy(gameObject);
                } 
                break;
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        { 
            case State.normal:

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") == false )
                {
                    
                    takeHit = false;
                                        
                    Collider2D enterInRange = Physics2D.OverlapCircle(range.position, rangeRadius, player);
                    
                    if (enterInRange!=null && cooldown == false)
                    {
                        actualTimer = timer;
                        attack = true;
                        state = State.attacking;

                    }
                    anim.SetBool("walk",true);
                    if (TargetInDistance() && followEnabled)
                    {
                        PathFollow();
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
            case State.death :
                break;
        }

    }
    

    private void UpdatePath()
    {
        if (!attack)
        {
            if (followEnabled && TargetInDistance() && seeker.IsDone())
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

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized; 
        force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }
        

        // Movement
        rb.AddForce(Vector2.right * direction, ForceMode2D.Impulse);

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
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        
        if (!p.error)
        {
            
            path = p;
            currentWaypoint = 0;
        }
    }

    void finishAnim()
    {
        attack = false;
        cooldown = true;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(range.position,rangeRadius);
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && attack && (takeHit == false))
        {
            takeHit = true;
            col.GetComponent<HealthSystem>().TakeDamage(5);
        }
    }

    void isDead()
    {
    
        dead = true;
    }
    
}