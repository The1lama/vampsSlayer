
using System.Collections;
using UnityEngine;
using PrimeTween;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{ 
    
    public PlayerScriptableObject playerSo;
    
    private SpriteRenderer _spriteRenderer;
    private HealthScript _healthScript;
    private PlayerAttack _playerAttack;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    public healthBar healthBar;
    
    private bool _isInvincible;
    private float _iFrameTime;
    private Color _hitTint;
    

    void Start()
    {
        _spriteRenderer =  GetComponent<SpriteRenderer>();
        _healthScript = GetComponent<HealthScript>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        
        _healthScript.SetMaxHealth(playerSo.health);
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());
        
        // Player Behaviour
        _iFrameTime = playerSo.iFrameTime;
        _hitTint = playerSo.hitTint;
        
        // Player Attack
        _playerAttack.strenght =  playerSo.strenght;
        _playerAttack.meleeSpeed = playerSo.attackSpeed;
        
        // Player Movement
        _playerMovement.speed = playerSo.speed;
        
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
            _animator.SetBool("isDead", true);
            
            GameManager.Instance.GameOver();
        }
        
        StartCoroutine(IFrames());
    }
    
    void HurtAnimation()
    {
        Tween.ShakeCamera(Camera.current, strengthFactor: 0.5f);
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
