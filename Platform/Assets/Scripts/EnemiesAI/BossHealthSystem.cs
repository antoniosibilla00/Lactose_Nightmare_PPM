using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{
    [SerializeField] HealthBar healthBar;
    private SpriteRenderer _renderer;
    //vita del player
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    private int healing;
    private bool hasTakenDamage;
    private float timerDamage;
    private float actualTimerDamage;
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetHealthBarMaxValue(maxHealth);
        _renderer = GetComponentInParent<SpriteRenderer>();
        currentHealth = maxHealth;
        healing = 20;
        hasTakenDamage = false;

    }

    // Update is called once per frame

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth -damage<0 )
        {
            currentHealth = -1;
        }
        else
        {
            StartCoroutine(BecomeTemporarilyRed());
            currentHealth -= damage;
            
        }
        
        healthBar.SetHealthBar(currentHealth);
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
    
    private IEnumerator BecomeTemporarilyRed()
    {
        Debug.Log("siumCOlorato");
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.3f);
        _renderer.color = new Color(255, 255, 255, 255);

    }
    
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
}
