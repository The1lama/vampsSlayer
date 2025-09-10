public class HealthUnit
{
    // fields
    private int _currentHealth;
    private int _maxHealth;

    
    // Properties, 
    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
   
    
    // Constructor
    public HealthUnit(int health, int maxHealth)
    {
        _currentHealth = health;
        _maxHealth = maxHealth;
    }
    
    // Methods
        // method to take damage
    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            CurrentHealth -= damage;
        }
    }

        // method to heal
    public void Heal(int heal)
    {
        if (CurrentHealth < _maxHealth)
        {
            CurrentHealth += heal;
        }
    }
}
