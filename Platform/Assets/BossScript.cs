using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using Pathfinding;
using UnityEditor;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class BossScript
    : MonoBehaviour
{
    [Header("Pathfinding")]
    private Transform target;
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
    private int currentWaypoint;
    #region AI variables
    private GameObject Alexander;
    [SerializeField]private float followDistance;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Transform range;
    [SerializeField] private float rangeRadius;
    [SerializeField] private Transform rangeLeap;
    [SerializeField] private float rangeLeapRadius;
    [SerializeField] private LayerMask player;
    [SerializeField] private LayerMask ground;
    private BoxCollider2D playerBoxCollider2D; 
    private CapsuleCollider2D playerCapsuleCollider2D;
    private BoxCollider2D meleeEnemyCollider;
    private Rigidbody2D playerBody;
    private int attackRandomizer;
    private int moveDir;
    private float cooldownLeap;
    private float actualCooldownLeap;
    private float positionUpdate;
    public GameObject king;
    
    
    [SerializeField]private float timer;
    #endregion

    #region Animator variables
    private bool attack ;
    private bool takeHit;
    private bool dead;
    private bool walk;
    private bool beastMode;
    private bool dash;
    bool facingRight;
    private bool done;
    private bool isNotPlaying;
    private Vector2 force;
    private bool cooldown=false;
    private bool canLeap;
    private float actualTimer;
    private BossHealthSystem healthSystem;
    [SerializeField] public BoxCollider2D hammerCollider2D;
    [SerializeField] public CapsuleCollider2D swordCollider2D;
    #endregion

    
    public enum State
    { 
        normal,
        attacking,
        death,
        idle,
        beast
    }
    
    public State state;
    


    public void Awake()
    {
        dead = false;
        Alexander = GameObject.FindGameObjectWithTag("Player");
        rb = Alexander.GetComponent<Rigidbody2D>();
        playerBoxCollider2D = Alexander.GetComponent<BoxCollider2D>();
        playerCapsuleCollider2D = Alexander.GetComponent<CapsuleCollider2D>();
        target = Alexander.GetComponent<Transform>();
    }

    public void Start()
    {
        meleeEnemyCollider = GetComponentInChildren<BoxCollider2D>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        Physics2D.IgnoreCollision(playerBoxCollider2D,meleeEnemyCollider);
        Physics2D.IgnoreCollision(playerCapsuleCollider2D,meleeEnemyCollider);
        healthSystem = GetComponentInChildren<BossHealthSystem>();
        attack = false;
        moveDir = 1;
        done = false;
        beastMode = false;
        cooldownLeap = 20f;
        cooldown = false;
  
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void OnEnable()
    {
        meleeEnemyCollider = GetComponentInChildren<BoxCollider2D>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        Physics2D.IgnoreCollision(playerBoxCollider2D,meleeEnemyCollider);
        Physics2D.IgnoreCollision(playerCapsuleCollider2D,meleeEnemyCollider);
        healthSystem = GetComponentInChildren<BossHealthSystem>();
        attack = false;
        moveDir = 1;
        done = false;
        beastMode = false;
        cooldownLeap = 20f;
        cooldown = false;
  
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        Debug.Log("done"+done);
        Debug.Log("moveDir"+moveDir);
        Debug.Log("target"+target);
     
        Debug.Log("bossattack"+attack);
        Debug.Log("walk"+walk);
        Debug.Log("cooldownBoss"+cooldown);
        Debug.Log("dash"+dash);
        
       
        anim.SetBool("attack",attack);
        anim.SetBool("cooldown",cooldown);
        anim.SetBool("walk",walk);
        anim.SetInteger("attackN",attackRandomizer);
        anim.SetBool("dash", dash);
        
        isNotPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName("dead") == false;
        
        if (healthSystem.GetCurrentHealth() <= 0 &&!dead && isNotPlaying)
        {
            anim.SetTrigger("dead");
            state = State.death;
        }

        isNotPlaying = anim.GetCurrentAnimatorStateInfo(0).IsName("introStage2") == false;
        
        if (healthSystem.GetCurrentHealth()<=(healthSystem.GetMaxHealth()/2) &&!done && isNotPlaying)
        {
            anim.SetBool("beastMode", beastMode);
            anim.SetTrigger("taunt");
            beastMode = true;

        }

        #region Manage Enemy states (In terms of Animation)

        if (!done && beastMode) return;
        switch (state)
        {
            case State.death :

                this.GetComponentInChildren<CapsuleCollider2D>().enabled= false;
                
                king.SetActive(true);
               
                if (dead)
                {
                    Destroy(gameObject);
                } 
                break;
            case State.idle:
              
                walk = false;
               
                break;
            case State.attacking:
                
                actualTimer -= Time.deltaTime;
                if (actualTimer<=0)
                {
                    Debug.Log("<<<sonoEntratoQua"+actualTimer);
                    cooldown = false;
                    state = State.normal;
                }
                break;
                
        }

        #endregion
        
      
    }

    private void FixedUpdate()
    {
        Debug.Log("<<<<2"+dashIsNotColliding());
        Debug.Log("<<<<1"+leapIsNotColliding());
        Debug.Log("<<<SonoEntratoFixed");
        if (actualCooldownLeap>=0)
        {
            actualCooldownLeap -= Time.deltaTime;
            
        }
        else
        {
            canLeap = true;
        }
        
        #region Manage Enemy states (Physics)

        if (!done && beastMode) return;
        
        
        switch (state)
        { 
            case State.normal:
                    
                    

                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") == false && anim.GetCurrentAnimatorStateInfo(0).IsName("attack2roll") == false  ) 
                {
                        
                    takeHit = false;
                                            
                    Collider2D enterInRange = Physics2D.OverlapCircle(range.position, rangeRadius, player);
                    Collider2D enterInLeapRange = Physics2D.OverlapCircle(rangeLeap.position, rangeLeapRadius, player);
                    Debug.Log("<<<SonoEntratoEvoglioUccidermi");
                        
                    if (enterInRange!=null && cooldown == false && healthSystem.GetCurrentHealth()>0)
                    {
                        actualTimer = timer;
                            
                        Debug.Log("<<<SonoEntratoAttack");
                        state = State.attacking;

                        attack = true;
                                
                        if (Random.value<0.5f)
                        {
                            attackRandomizer = 0;
                        }
                        else
                        {
                            attackRandomizer = 1;
                                    
                        }
                                
                        FlipWhenInRange();

                    }
                            
                            
                    else if (enterInLeapRange!=null&& cooldown == false && healthSystem.GetCurrentHealth()>0 && TriggerLeap()&& beastMode)
                    {
                               
                        actualTimer = timer;
                        Debug.Log("<<<SonoEntratoLeap");
                        attack = true;
                        attackRandomizer = 2;
                        state = State.attacking;
                        FlipWhenInRange();
                                
                    }
                            
                    else if(BecameInactive())
                    {
                        state = State.idle;
                            
                    }
                    else if (TargetInDistance() && followEnabled)
                    {
                        Debug.Log("<<<EccoLaPunta"+(TargetInDistance() && followEnabled));
                        if (TriggerDash() && beastMode)
                        {
                            dash = true;
                            walk = false;
                                   
                        }
                        else
                        {
                            walk = true;
                            dash = false;


                        }
                                
                        PathFollow();
                    }
                            
                           
                            
                }

                break;
                
            case State.attacking:
             
                   
                if (actualTimer <= 0 )
                {
                    cooldown = false;
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
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponentInChildren<BoxCollider2D>().bounds.extents.y + jumpCheckOffset);
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
            if (rb.velocity.x > 0.05f && facingRight)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                facingRight = false;
                moveDir = 1;
            }
            else if (rb.velocity.x < -0.05f && !facingRight)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                facingRight = true;
                moveDir = -1;
            }
        }
    }
    
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }


    private bool TriggerDash()
    {
       
        return (Vector2.Distance(transform.position, target.transform.position) >= (followDistance*0.5)) && dashIsNotColliding();
    }
    
    private bool BecameInactive()
    {
        return Vector2.Distance(transform.position, target.transform.position) > followDistance;
    }
    

    private void OnPathComplete(Path p)
    {
        
        if (!p.error)
        {
            
            path = p;
            currentWaypoint = 0;
        }
    }

    void StartAnim()
    {
        hammerCollider2D.enabled = true;
        swordCollider2D.enabled = true;


    }
    
    private bool TriggerLeap()
    {
        float rand = Random.value;
        
      
        if (rand>=0.8f && canLeap && leapIsNotColliding())
        {
            canLeap = false;
            actualCooldownLeap = cooldownLeap;
            return true;
            
        }
        return false;
    }

    
    private bool leapIsNotColliding()
    {
        //point,size,direction,angle
        Collider2D leap_ground = Physics2D.OverlapCapsule(rangeLeap.position,new Vector2(5.84576f,0.6838409f),CapsuleDirection2D.Horizontal,0,ground);
        if (leap_ground != null)
        {
            return false;
        }

        return true;
    }
    
    private bool dashIsNotColliding()
    {
        //point,size,direction,angle
        Collider2D leap_ground = Physics2D.OverlapCapsule(rangeLeap.position,new Vector2(2.465975f,0.6838409f),CapsuleDirection2D.Horizontal,0,ground);
        if (leap_ground != null)
        {
            return false;
        }

        return true;
    }
    
    
    
    

    private void FlipWhenInRange()
    {
        if (target.position.x > transform.position.x)
        { 
            facingRight = false;
            transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            moveDir = 1;
        }else{
            facingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            moveDir = -1;

        }
    }

    void RollingThunder()
    {
        rb.AddForce(new Vector2(200*moveDir,0));
    }
    
    void finishDash()
    {
        dash = false;
    }

    void AnimationDone()
    {
        done = true;
    }
    void finishAnim()
    {
        Debug.Log("<<<SonoEntratoFinish");
        attack = false;
        cooldown = true;
        walk = false;
        dash = false;
        Debug.Log("cooldownFunzione"+cooldown);
        swordCollider2D.enabled = false;
        hammerCollider2D.enabled = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(range.position,rangeRadius);
        Gizmos.DrawWireSphere(rangeLeap.position,rangeLeapRadius);

    }
    
    void changePosition(float offset)
    {
        Debug.Log("offset"+offset);
        if (facingRight)
        {
            transform.position = new Vector3(transform.position.x-offset,transform.position.y, 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x+offset,transform.position.y, 0);
        }
    }


    void BecameDead()
    {
        dead = true;
    }
}