using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int currentHealth;

    [SerializeField] private int maxHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void SetCurrentHealth( int newHealth)
    {
        currentHealth = newHealth;
    }

    public void TakeDamage(int damage)
    
        {
            if (GetCurrentHealth() -damage<0 )
            {
                SetCurrentHealth(0);
            }
            else
            {
                SetCurrentHealth(GetCurrentHealth() - damage);
          
            }
            
        }
    
}
