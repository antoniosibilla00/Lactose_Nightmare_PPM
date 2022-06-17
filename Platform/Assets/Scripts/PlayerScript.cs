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
    private float yInput;

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
    private bool checkpoint;
    [SerializeField]
    private float invincibilityDurationSeconds;

    private int moveDir;
    private float rollSpeed;

    public Transform attackPoint;
    [SerializeField] private float attackRange;
    public LayerMask enemyLayers;
    public static PlayerScript instance;
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
        instance = this;
    }

    private void Start()
    {
        
        _renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        state = State.normal;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        transform.position = gm.lastCheckPointPos;
        moveDir = 1;
        sourceSium = GetComponent<AudioSource>();
        LoadPlayer();
        Debug.Log("percorso:"+Application.persistentDataPath);


    }


    // metodo che gestisce gli input e le animazioni
    private void Update()
    {
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
                    
                    state = State.normal;
                    
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

                xInput = Input.GetAxis("Horizontal");
                yInput = Input.GetAxis("Vertical");
                run = xInput != 0;


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
                
                body.AddForce(new Vector2(9*moveDir,0));

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

        if (Input.GetMouseButtonDown(0) && IsGrounded() && !attack)
        {
            anim.SetTrigger(""+combo);
            OnClick();
            attack = true;
            state = State.attacking;

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

        if (Input.GetKeyDown((KeyCode.I)) && roll != true)
        {
            healthSystem.TakeDamage(10);
          
        }
    }

    private void OnClick()
    {
        GolemAI enemyIstance;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.GetComponent<GolemAI>().golemHealth - 10<0)
            {
                enemy.GetComponent<GolemAI>().golemHealth = -1;
            }
            else
            {
                enemy.GetComponent<GolemAI>().golemHealth -=10;
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
        if (combo < 3)
        {
           
           /* sourceSium.clip = sium[combo];
            sourceSium.Play();*/
            combo++;
        }
    }


    public void FinishAnim()
    {
        attack = false;
        combo = 0;
    }
    
    
    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        _renderer.color = Color.red;
        healthSystem.isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        _renderer.color = new Color(255, 255, 255, 255);
        healthSystem.isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }
    
    void TriggersInvulnerability()
    {
        if (!healthSystem.isInvincible)
        {
            StartCoroutine(BecomeTemporarilyInvincible());
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

   
   private void OnTriggerEnter2D(Collider2D col)
   {
       if (col.gameObject.name.Equals("Nemico") )
       {
           healthSystem.TakeDamage(10);
       }
      
   }
   

   public void SavePlayer()
   {
       SaveSystem.SavePlayer(this);
   }

   public void LoadPlayer()
   {
      PlayerData player= SaveSystem.LoadPlayer(this);
      if (player != null)
      {
          Vector2 position = new Vector2(player.position[0], player.position[1]);
          this.GetComponent<Transform>().position = position;

          healthSystem.SetHealth(player.health);
          this.level = player.level;

      }
   }


   bool isTalking()
   {
       return (dialoguePanel.activeSelf);
   }
   
}
