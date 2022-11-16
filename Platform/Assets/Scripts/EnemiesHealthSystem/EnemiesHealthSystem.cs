using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesHealthSystem : MonoBehaviour
{
    // Start is called before the first frame update
    
    private int currentHealth;
    private SpriteRenderer _renderer;
    [SerializeField] private int maxHealth;
    private float timer=5f;
    private float actualTimer;
    private bool done;
    public AudioClip getDamaged;
    public AudioSource AudioSource;
    private void Start()
    {
        _renderer = GetComponentInParent<SpriteRenderer>();
        currentHealth = maxHealth;
        AudioSource = GetComponentInParent<AudioSource>();
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
        AudioSource.clip= getDamaged;
        done = true;
        if (GetCurrentHealth() -damage<0 )
        {
            SetCurrentHealth(0);
        }
        else
        {
            SetCurrentHealth(GetCurrentHealth() - damage);
          
            AudioSource.Play();
      
        }

        
        
        StartCoroutine(BecomeTemporarilyRed());



    }



    public int GetMaxHealth()
    {
        return maxHealth;
    }
    private IEnumerator BecomeTemporarilyRed()
    {
        Debug.Log("siumCOlorato");
        _renderer.color = Color.red;

        yield return new WaitForSeconds(0.3f);
        _renderer.color = new Color(255, 255, 255, 255);

    }

    public void RestoreHealth()
    {
        SetCurrentHealth(maxHealth);
    }

 
    
}
