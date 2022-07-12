using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;

    [SerializeField] private HealthPotions healthPotions;

    public GameObject blood;
    private bool finishToBlood;
    private Transform alexanderPos;
    private AudioSource AudioSource;
    public AudioClip takeHurt;
    public AudioClip healingSound;
    public static HealthSystem Instance{get; private  set; }

    private SpriteRenderer _renderer;
    //vita del player
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private int healing;
    private int flasks;
    private bool hasTakenDamage;
    private float timerDamage;
    private float actualTimerDamage;

    public bool isInvincible;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        alexanderPos = GetComponentInParent<Transform>();
        AudioSource = GetComponentInParent<AudioSource>();
    }

    void Start()
    {
        
        healthBar.SetHealthBarMaxValue(maxHealth);
        _renderer = GetComponent<SpriteRenderer>();
        
        currentHealth = maxHealth;
        healing = 20;
        flasks = 0;
        isInvincible = false;
        hasTakenDamage = false;

    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("currentHealth = " + currentHealth);
        Debug.Log(" health = "+ maxHealth);
        if (flasks <3 && currentHealth<100)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Healing();
                flasks++;

            }
        }
        

        

    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            Debug.Log("!danno");
            if (currentHealth -damage<0 )
            {
                currentHealth = -1;
            }
            else
            {
                StartCoroutine(BecomeTemporarilyInvincible(1.5f));
                currentHealth -= damage;
                AudioSource.clip = takeHurt;
                AudioSource.Play();
               
            }
            CinemachineShake.Instance.ShakeCamera(0.5f,0.5f); 
            GameObject newBlood= GameObject.Instantiate(blood,alexanderPos);
            StartCoroutine(DestroyBlood(newBlood));
            healthBar.SetHealthBar(currentHealth);
            

        }
       
    }

    public void RestoreHealthAndPotions()
    {
        healthBar.SetHealthBar(maxHealth);
        healthPotions.SetPotionsFill(0);
        healthPotions.SetPotionsFill(1);
        healthPotions.SetPotionsFill(2);
        flasks = 0;
    }

    public void Healing()
    {
        Debug.Log("cura");
    
        if (currentHealth + healing > 100)
        {
            currentHealth = 100;
        }
        else
        {
            currentHealth += healing;
          
        }

        StartCoroutine(BecomeTemporarilyGreen());
        AudioSource.clip = healingSound;
        AudioSource.Play();

        healthBar.SetHealthBar(currentHealth);
        healthPotions.SetPotionsEmpty(flasks);
        
    }
    public void SetHealth(int health)
    {
       
        healthBar.ResizeHealthBar(health);
        maxHealth = health;
        
        if (health != 100 )
        {
            currentHealth = health;
            healthBar.SetHealthBar(health);
            
        }
        else
        {
            if (currentHealth > 100)
            {
                healthBar.SetHealthBar(health);
                currentHealth = health;
            }
            else
            {
                healthBar.SetHealthBar(currentHealth);
            }
            
        }
    }
    
    public IEnumerator BecomeTemporarilyInvincible(float invincibilityDurationSeconds)
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(11,12,true);
     
        
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        
        Physics2D.IgnoreLayerCollision(11,12,false);
        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }

    
    public IEnumerator DestroyBlood(GameObject blood)
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(blood);
 
    }
    
     public IEnumerator BecomeTemporarilyGreen()
    {
        Debug.Log("Player turned invincible!");
        _renderer.color = Color.green;
     
        
        yield return new WaitForSeconds(1.2f);
    
        _renderer.color = new Color(255, 255, 255, 255);
        Debug.Log("Player is no longer invincible!");
    }
    
    
}
