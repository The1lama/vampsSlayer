using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(int _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    private void MaxToCurrent()
    {
        currentHealth = maxHealth;
    }
    
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Healing(int heal)
    {
        currentHealth += heal;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
}
