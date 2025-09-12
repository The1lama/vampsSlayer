using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private int maxHealth;
    private int _currentHealth;


    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void Healing(int heal)
    {
        _currentHealth += heal;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }
    
}
