using System.Collections;
using UnityEngine;
using PrimeTween;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour, IDamageable
{ 
    
    public PlayerScriptableObject playerSo;
    public healthBar healthBar;
    
    private SpriteRenderer _spriteRenderer;
    private HealthScript _healthScript;
    private MeleeAttack _meleeAttack;
    private ShotGunAttack _gunAttack;
    private PlayerMovement _playerMovement;
    private Animator _animator;
    
    [SerializeField]
    private bool _isInvincible;
    private float _iFrameTime;
    private Color _hitTint;
    
    void Start()
    {
        #region GetComponents

                _spriteRenderer =  GetComponent<SpriteRenderer>();
                _healthScript = GetComponent<HealthScript>();
                _meleeAttack = GetComponentInChildren<MeleeAttack>();
                _gunAttack = GetComponentInChildren<ShotGunAttack>();
                _playerMovement = GetComponent<PlayerMovement>();
                _animator = GetComponent<Animator>();

        #endregion
        
        Initialize();
    }

    private void Initialize()
    {
        // set player health
        _healthScript.SetMaxHealth(playerSo.health);
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());
        
        // Player Behaviour
        _iFrameTime = playerSo.iFrameTime;
        _hitTint = playerSo.hitTint;
        
        // Player Attack
        _meleeAttack.SetMeleeSpeed(playerSo.attackSpeed);
        _gunAttack.SetMeleeSpeed(playerSo.attackSpeed);

        SetAttackStrenght(playerSo.strenght);
        
        // Player Movement
        _playerMovement.SetSpeed(playerSo.speed);
    }
    
    public void SetNewMeleeSpeed(float attackSpeed)
    {
        _meleeAttack.SetNewMeleeSpeed(attackSpeed);
        _gunAttack.SetNewMeleeSpeed(attackSpeed);
    }

    public void SetAttackStrenght(int strenght)
    {
        _meleeAttack.SetStrenght(strenght);
        _gunAttack.SetStrenght(strenght);
    }
    
    public void SetNewMaxHealth(int maxHealth)
    {
        _healthScript.SetMaxHealth(maxHealth);
        healthBar.SetMaxHealth(_healthScript.GetMaxHealth());

    }
    
    public void TakeDamage(int strength)
    {
        if (_isInvincible)  return;     // if player has iFrames do nothing; // Coroutine IFrames switches _isInvincible statement 
        
        _healthScript.TakeDamage(strength);
        healthBar.SetHealth(_healthScript.GetCurrentHealth());
        
        if (_healthScript.GetCurrentHealth() <= 0)
        {
            _animator.SetBool("isDead", true);
            
            GameManager.Instance.onDead?.Invoke();
        }
        
        StartCoroutine(IFrames());
    }

    #region When Hurt Funictions

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
        
    #endregion
    

}
