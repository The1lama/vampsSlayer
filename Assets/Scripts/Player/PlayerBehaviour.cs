
using System.Collections;
using UnityEngine;
using PrimeTween;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{ 
    
    private SpriteRenderer _spriteRenderer;

    private bool _isInvincible;
    [SerializeField] private float iFrameTime;
    [SerializeField] private Color hitTint = Color.red;
    [SerializeField] private int maxHealth;
    
    private HealthScript _healthScript;
    
    public healthBar healthBar;
    
    void Start()
    {
        _spriteRenderer =  GetComponent<SpriteRenderer>();
        _healthScript = GetComponent<HealthScript>();
        
        _healthScript.SetMaxHealth(maxHealth);
        _healthScript.CurrentToMax();
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());
        
    }
    
    private void PlayerHeal(int healing)
    {
        _healthScript.Healing(healing);
        healthBar.SetHealth(_healthScript.GetCurrentHealth());
    }
    
    public void TakeDamage(int strength)
    {
        if (_isInvincible)  return;     // if player has iFrames do nothing
        
        _healthScript.TakeDamage(strength);
        healthBar.SetHealth(_healthScript.GetCurrentHealth());
        
        if (_healthScript.GetCurrentHealth() <= 0)
        {
            Debug.LogError("Game Over Bitch");
            GameManager.Instance.GameOver();
        }
        
        StartCoroutine(IFrames());
    }
    
    void HurtAnimation()
    {
        Sequence.Create()
            .Group(Tween.Color(_spriteRenderer, hitTint, 0.1f))
            .ChainDelay(0.5f)
            .Group(Tween.Color(_spriteRenderer, Color.white, iFrameTime));
    }
    
    
    private IEnumerator  IFrames()
    {
        _isInvincible = true;
        HurtAnimation();
        yield return new WaitForSeconds(iFrameTime);
        _isInvincible = false;
    }
    
}
