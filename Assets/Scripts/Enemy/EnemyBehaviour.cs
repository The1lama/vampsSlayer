using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int ScoreAmount;
    public int Health;
    public int Strenght;

    // public HealthUnit UnitHealth;

    // public HealthUnit UnitHealth = new HealthUnit(Health, Health);
    
    public HealthUnit UnitHealth = new HealthUnit(50, 50);

    
    private void Start()
    {
        // gives enemy health with variable set.
        // HealthUnit UnitHealth = new HealthUnit(Health, Health);
    }
   
    public void EnemyTakeDamage(int damage)
    {
        Debug.Log("EnemyTakeDamage");
        Debug.Log("Health: " + UnitHealth.CurrentHealth);
        UnitHealth.TakeDamage(damage);
        
        if (UnitHealth.CurrentHealth > 0) return;
        
        // Debug.Log("Enemy Dead");
        // GameManager.gameManager.AddScore(10);
        // GameManager.gameManager.AddExperiencePoints(5);
        
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Debug.Log("OnTriggerStay2D");
        if (collision.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Player");
            collision.GetComponent<PlayerBehaviour>().PlayerTakeDamage(Strenght);
        }
    }
    
    
    
}
