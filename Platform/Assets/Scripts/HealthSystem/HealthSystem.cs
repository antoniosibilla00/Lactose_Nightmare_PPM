using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;

    [SerializeField] private HealthPotions healthPotions;
    //vita del player
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private int healing;
    private int flasks;

    public bool isInvincible;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetHealthBarMaxValue(maxHealth);
        currentHealth = maxHealth;
        healing = 20;
        flasks = 0;
        isInvincible = false;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("health "+currentHealth);
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
            if (currentHealth -damage<0 )
            {
                currentHealth = 0;
            }
            else
            {
                currentHealth -= damage;
          
            }

            healthBar.SetHealthBar(currentHealth-damage);
            currentHealth -= damage;
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
    
        if (currentHealth + healing>100)
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
    public void SetHealth(int health){
        healthBar.SetHealthBar(health);
    }
}
