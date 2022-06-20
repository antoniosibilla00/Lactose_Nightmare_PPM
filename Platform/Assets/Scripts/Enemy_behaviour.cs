using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy_behaviour : MonoBehaviour
{
    #region Public Variables

    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLenght;
    public float attackDistace; //Distanza minima per attacare
    public float moveSpeed;
    public float timer; //CoolAntonio prima di attacare
    public Transform leftLimit;
    public Transform rightLimit;

    #endregion

    #region Private Variables

    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance; //Conserva la distanza tra nemico e player
    private bool attackMode;
    private bool inRange; //Controlla se il Player è in range
    private bool cooling; //Controlla se il nemico è fermo(cooling) dopo l'attacco
    private float intTimer;

    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; //Conserva il valore iniziale del timer
        anim = GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {

        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attackChocolateSium"))
        {
            SelectTarget();


        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLenght, rayCastMask);
            RaycastDebugger();

        }

        //Quando Player è stato individuato
        if (hit.collider != null)
        {
            EnemyLogic();

        }
        else if (hit.collider == null)
        {

            inRange = false;
        }

        if (inRange == false)
        {
            //anim.SetBool("canWalk", false);
            StopAttack();
        }

        Debug.Log("Anim.GetCurrent = " + anim.GetCurrentAnimatorStateInfo(0).IsName("attackChocolateSium"));

        Debug.Log("Attack distance = " + attackDistace);
        Debug.Log("distance = " + distance);
        
        
        Debug.Log("transform.position.x = " + transform.position.x);
        Debug.Log("leftLimit.position.x = " + leftLimit.position.x);
        Debug.Log("target = " + target);
        
        
        

    }

    private void OnTriggerEnter2D(Collider2D trig)
    {

        if (trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip(1);
        }


    }

    void EnemyLogic()
    {

        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistace)
        {

            StopAttack();

        }
        else if (attackDistace >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }

    }

    void Move()
    {
        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attackChocolateSium"))
        {

            Debug.Log("sium del sium");
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y);
            Debug.Log("targetPosition = " + targetPosition );

            if (inRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, (moveSpeed + 0.3f) * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
           

        }

    }

    void Attack()
    {
        timer = intTimer; //Reset timer quando il Player entra nel range di attacco
        attackMode = true; //Controlla se  il nemico puo attacare o no 

        anim.SetBool("canWalk", false);
        anim.SetBool("attack", true);

    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);
    }

    void RaycastDebugger()
    {
        if (distance < attackDistace)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLenght, Color.red);


        }
        else if (attackDistace < distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLenght, Color.green);


        }


    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    private bool InsideofLimits()
    {
        Debug.Log("transform.position.x  = " + (transform.position.x ));
        Debug.Log("leftLimit.position.x = " + ( leftLimit.position.x  ));
        Debug.Log(" rightLimit.position.x = " +  (rightLimit.position.x ));
        Debug.Log(" transform.position.x > leftLimit.position.x && transform.position.x > rightLimit.position.x " +  (transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x ));
        return transform.position.x > leftLimit.position.x && transform.position.x  < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        Debug.Log(" distanceToLeft = " +  (distanceToLeft ));
        Debug.Log(" distanceToRight = " +  (distanceToRight ));

        bool iHaveChanged;
        
        if (distanceToLeft  > distanceToRight || distanceToRight == distanceToLeft)
        {
            
            target = leftLimit;

        }else
        {

            target = rightLimit;

        }   
        
        //Flip(2);
        

    }


 

    private void Flip(float temp)
    {
        if (temp == 1)
        {
            if(transform.position.x > target.position.x ){
            
                transform.Rotate(new Vector3(0,0,0));

            }else{
           
                transform.Rotate(new Vector3(0,180,0));


            }
        }
        else if(temp == 2)
        {
            if(target == leftLimit ){
            
                transform.Rotate(new Vector3(0,0,0));

            }else if(target == rightLimit){
           
                transform.Rotate(new Vector3(0,180,0));


            }
        }
        
       
        
    }




}