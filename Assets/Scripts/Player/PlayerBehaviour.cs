using System.Collections;
using UnityEngine;
using PrimeTween;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{ 
    
    public PlayerScriptableObject playerSo;
    
    private SpriteRenderer _spriteRenderer;
    private HealthScript _healthScript;
    private PlayerAttack _playerAttack;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    public healthBar healthBar;
    
    [SerializeField]
    private bool _isInvincible;
    private float _iFrameTime;
    private Color _hitTint;
    
    void Start()
    {
        _spriteRenderer =  GetComponent<SpriteRenderer>();
        _healthScript = GetComponent<HealthScript>();
        _playerAttack = GetComponentInChildren<PlayerAttack>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        
        // set player health
        _healthScript.SetMaxHealth(playerSo.health);
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());
        
        // Player Behaviour
        _iFrameTime = playerSo.iFrameTime;
        _hitTint = playerSo.hitTint;
        
        // Player Attack
        _playerAttack.SetStrenght(playerSo.strenght);
        _playerAttack.SetMeleeSpeed(playerSo.attackSpeed);
        
        // Player Movement
        _playerMovement.SetSpeed(playerSo.speed);
        
    }

    public void SetNewMaxHealth(int maxHealth)
    {
        _healthScript.SetMaxHealth(maxHealth);
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());

    }
    
    private void PlayerHeal(int healing)
    {
        _healthScript.Healing(healing);
        healthBar.SetHealth(_healthScript.GetCurrentHealth());
    }
    
    public void TakeDamage(int strength)
    {
        if (_isInvincible)  return;     // if player has iFrames do nothing; // Coroutine IFrames switches _isInvincible statement 
        
        _healthScript.TakeDamage(strength);
        healthBar.SetHealth(_healthScript.GetCurrentHealth());
        
        if (_healthScript.GetCurrentHealth() <= 0)
        {
            _animator.SetBool("isDead", true);
            
            GameManager.Instance.onDeath?.Invoke();
        }
        
        StartCoroutine(IFrames());
    }
    
    void HurtAnimation()
    {
        Sequence.Create()
            .Group(Tween.Color(_spriteRenderer, _hitTint, 0.1f))
            .ChainDelay(0.5f)
            .Group(Tween.Color(_spriteRenderer, Color.white, _iFrameTime));
    }
    
    
    private IEnumerator  IFrames()
    {
        _isInvincible = true;
        HurtAnimation();
        yield return new WaitForSeconds(_iFrameTime);
        _isInvincible = false;
    }
    
}
