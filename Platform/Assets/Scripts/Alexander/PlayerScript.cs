using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //velocita personaggio
    [SerializeField] public float speed;

    //forza applicata nel salto
    [SerializeField] private float jumpForce;

    //layer contenente tutti gli oggetti che rappresentano il ground
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject dialoguePanel;



    //layer contenente tutti gli oggetti che rappresentano il wall
    // [SerializeField] private LayerMask wallLayer;

    //riferimento alla barra della vita
  

    //flag per controllare se il player si rivolge verso destra
    private bool facingRight = true;

    //gestisce le  animazioni
    public Animator anim;

    //gestisce gli input provenienti dall'asse x
    private float xInput;

    //gestisce gli input provenienti dall'asse y


    //consente di attuare la gravità nell'oggetto
    private Rigidbody2D body;

    //consente di attuare le collisioni
    private BoxCollider2D boxCollider;
    private SpriteRenderer _renderer;


    //blocco variabili per la gestione del salto anticipato
    private float hangTime = 2f;
    private float hangTimeCounter;
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool grounded;
    private bool jump;
    private bool run;
    private bool attack;
    private bool roll;
    private bool move;
    [SerializeField]
    private float invincibilityDurationSeconds;

    private int moveDir;
    private float rollSpeed;

    public Transform attackPoint;
    [SerializeField] private float attackRange;
    public LayerMask enemyLayers;
    public static PlayerScript instance;
    public LayerMask playerLayerMask;
    public LayerMask SafezoneLayerMask;
    
    public HealthSystem healthSystem;
    public int level;
    private int combo;
    
    public GameMaster gm;
    public AudioClip[] sium;
    public AudioSource sourceSium;



    public enum State
    {
        normal,
        rolling,
        attacking,
        death,
        talking
    }




    public State state;
    // Start is called before the first frame update

    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(11, 6);
        switch ( (SceneManager.GetActiveScene().name))
        {
            case "Level2":
                level=2;
                break;
            case "SampleScene":
                level = 1;
                break;
            case "Level3":
                level = 3;
                break;
        }
        
       
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        if (!LoadPLayer())
        {
            this.transform.position = gm.lastCheckPointPos;
        }
        healthSystem = GetComponentInChildren<HealthSystem>();
        _renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponentInChildren<BoxCollider2D>();
        state = State.normal;
       
       moveDir = 1;
        sourceSium = GetComponent<AudioSource>(); 
       
        //Debug.Log("percorso:"+Application.persistentDataPath);
        move = true;
        instance = this;


    }
    


    // metodo che gestisce gli input e le animazioni
    private void Update()
    {
        Debug.Log("state"+state);
        if (isTalking() && state != State.talking)
        {
            state =  State.talking;
        }
        
        switch (state)
        {
            case State.normal:
                
                HandleInput();
                HandleAnimations();
                break;

            case State.rolling:
                TriggersInvulnerability();

                if (!roll)
                {
                    state = State.normal;
                    
                }
                
              
                break;


            case State.attacking:
                anim.SetBool("run",false);
                
                if (!attack)
                {

                    state =  State.normal;
                    
                }
                
                break;
            
            case State.death:
                if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
            case  State.talking:
           
                anim.SetBool("run",false);
                if (!dialoguePanel.activeSelf)
                {
                    state = State.normal;
                }
                break;

        }



    }

    //vanno messe le parti che sfruttano la fisica di unity
    private void FixedUpdate()
    {
        Debug.Log(state);
        switch (state)
        {
            case State.normal:
                xInput = Input.GetAxisRaw("Horizontal");

                if (move)
                {
                    run =   run = xInput != 0;
                    speed = 2;
                }
                else
                {
                    speed = 0;

                }
                body.velocity = new Vector2(xInput * speed, body.velocity.y);



                if (xInput > 0 && facingRight != true)
                {
                    moveDir = 1;
                    Flip();
                }

                if (xInput < 0 && facingRight )
                {
                    moveDir = -1;
                    Flip();
                }


                if (IsGrounded())
                {
                    grounded = true;
                    hangTimeCounter = hangTime;
                }
                else
                {
                    grounded = false;
                    hangTimeCounter -= Time.deltaTime;
                }
                
                if (jump)
                {
                    Jump();
                    jump = false;
                }

                break;

            case State.rolling:
                body.velocity=Vector2.zero;
                body.AddForce(new Vector2(moveDir*2,0),ForceMode2D.Impulse);

                break;
            case State.attacking:
                body.velocity = Vector2.zero;
                break;
            
            case State.death:

                break;


        }

    }

    private void Jump()
    {
        //ForceMode2D.Impulse is useful if Jump() is called using GetKeyDown
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("jump");
        sourceSium.clip = sium[3];
        sourceSium.Play();
        hangTimeCounter = 0f;
        jumpBufferCounter = 0f;

    }

    private void Flip()
    {

        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        facingRight = !facingRight;

    }

    private Boolean IsGrounded()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down,
            0.1f, groundLayer);
        return raycast.collider != null;
    }

    private void HandleAnimations()
    {
        anim.SetBool("run", run);
        /*
        if (!run)
        {
            sourceSium.Stop();
        }
        */
        if (IsGrounded())
        {
            anim.SetBool("grounded", true);


        }
        else
        {
            anim.SetBool("grounded", false);

            //Set the animator velocity equal to 1 * the vertical direction in which the player is moving 
            if (body.velocity.y < 0)
            {
                anim.SetTrigger("fall");
            }

        }

        if (healthSystem.GetCurrentHealth() <= 0)
        {
            anim.SetTrigger("death");
            sourceSium.clip= sium[0];
            sourceSium.Play();
            state = State.death;
        }
    }

    private void HandleInput()
    {

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;

        }
        else
        {
            jumpBufferCounter -= jumpBufferTime;
        }


        if (jumpBufferCounter > 0 && hangTimeCounter > 0)
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.X) && IsGrounded() && !attack)
        {
            body.velocity =(Vector2.zero);
            anim.SetTrigger(""+combo);
            attack = true;
            state = State.attacking;
            /*
            sourceSium.clip = sium[3];
            sourceSium.Play();
            
            */
        }

        if (Input.GetKeyDown(KeyCode.Z) && grounded)
        {
            anim.SetTrigger("roll");
            roll = true;
            rollSpeed = speed;
            state = State.rolling;
            
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        body.AddForce(new Vector2(moveDir *100,0));
        foreach (Collider2D enemy in hitEnemies)
        {

            if (enemy.name.Equals("BossHitbox"))
            {
                if (enemy.GetComponent<BossHealthSystem>().GetCurrentHealth() > 0)
                {
                    Debug.Log("collider:"+enemy.name);
                    enemy.GetComponent<BossHealthSystem>().TakeDamage(10);
                    enemy.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(moveDir*15,0));
                }
             
            }
            else
            {
                
                if (enemy.GetComponent<EnemiesHealthSystem>().GetCurrentHealth() > 0)
                {
                    Debug.Log("collider:"+enemy.name);
                    enemy.GetComponent<EnemiesHealthSystem>().TakeDamage(10);
                    enemy.GetComponentInParent<Rigidbody2D>().AddForce(new Vector2(moveDir*15,0));
                }
            }
           
           
        }  
        



    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    public bool IsAttacking()
    {
        return attack;
    }

    public bool IsRunning()
    {
        return run;
    }

    public void StartCombo()
    {
        attack = false ;
        body.velocity = Vector2.zero;
        if (combo < 3)
        {
           
            sourceSium.clip = sium[1];
            sourceSium.Play();
            combo++;
        }
    }

    public void FinalCombo()
    {
        sourceSium.clip = sium[1];
        sourceSium.Play();
    }


    public void FinishAnim()
    {
        attack = false;
        combo = 0;
    }
    
    
    
    
    
    
    

    
    void TriggersInvulnerability()
    {
        if (!HealthSystem.Instance.isInvincible)
        {
            StartCoroutine(HealthSystem.Instance.BecomeTemporarilyInvincible(invincibilityDurationSeconds));
        }
    }
    
   
   public void StartRoll()
   {
       roll = true;
   }
   

   public void FinishRoll()
   {
       Debug.Log("qui");
       roll = false;
   }

   
  
   /*private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.gameObject.name.Equals("Nemico") )
       {
           healthSystem.TakeDamage(10);
       }
      
   }
   */
   

   public void SavePlayer()
   {
       SaveSystem.SavePlayer(this);
   }




   bool isTalking()
   {
       return (dialoguePanel.activeSelf);
   }

   public void Debuff()
   {        
       
       healthSystem.SetHealth(50); 
       
   }
   
   public void Buff()
   {        
       
       healthSystem.SetHealth(200); 
       
   }

   
   public void Restore()
   {
      
           healthSystem.SetHealth(100); 
      
   }

   public void CanMove()
   {
       move = true;
   }

   public void walking()
   {
       /*
       if (!sourceSium.isPlaying)
       {
           sourceSium.clip = sium[2];
           sourceSium.Play();
       }
       */
       Debug.Log("dovrebbe cammminare");
      
   }
   

   public void CanNotMove()
   {
       move = false;
   }

   public bool LoadPLayer()
   {
      
       PlayerData player= SaveSystem.LoadPlayer();
       
       if (player != null)
       {
           Vector2 position = new Vector2(player.position[0], player.position[1]);

       
           this.transform.position = position;
           
           //healthSystem.SetHealth(player.health);
           
           return true;
       }
       
        return false;
       
   }

}
