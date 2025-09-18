using System;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    private int _maxHealth;
    private int _currentHealth;


    // private void Start()
    // {
    //     currentHealth = maxHealth;
    // }


    private void Update()
    {
        // Debug.Log(_maxHealth);
    }

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        // Debug.Log("Max Health: " + _maxHealth);
    }

    public void CurrentToMax()
    {
        _currentHealth = _maxHealth;
    }
    
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void Healing(int heal)
    {
        _currentHealth += heal;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
    
}
