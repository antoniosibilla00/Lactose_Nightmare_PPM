using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;

    [SerializeField] private HealthPotions healthPotions;

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
                StartCoroutine(BecomeTemporarilyInvincible(3f));
                currentHealth -= damage;
                
            }
            
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
        _renderer.color = Color.red;
        isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityDurationSeconds);
        _renderer.color = new Color(255, 255, 255, 255);
        isInvincible = false;
        Debug.Log("Player is no longer invincible!");
    }
    
    
}
