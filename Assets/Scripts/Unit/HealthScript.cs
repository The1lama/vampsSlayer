using System;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    private int _maxHealth;
    private int _currentHealth;

    public void SetMaxHealth(int maxHealth)
    {
        _maxHealth = maxHealth;
        CurrentToMax();
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
