using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") )
        {
            if (HealthSystem.Instance != null)
            {
                HealthSystem.Instance.TakeDamage(damage);
            }
           
            Debug.Log("instance"+HealthSystem.Instance);
        }
    }

    public void SetDamage(int newDamage)
    {
        this.damage = newDamage;
    }
}
