using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    
    private Rigidbody2D _rb;
    private HealthScript _healthScript;
    private MoveToPlayer _moveToPlayerScript;
    private Animator _animator;
    private PlayerBehaviour _player;
    // private SpriteRenderer _renderer;
    
    public EnemyScriptableObject statSo;
    
    private bool _isAttacking;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthScript = GetComponent<HealthScript>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerBehaviour>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();
        // _renderer = GetComponent<SpriteRenderer>();
        
        _healthScript.SetMaxHealth(statSo.health);
        _healthScript.CurrentToMax();
        _moveToPlayerScript.SetSpeed(statSo.speed);
    

    }

    public void TakeDamage(int strength)
    {
        _healthScript.TakeDamage(strength);
        _animator.SetTrigger("isHurt");
        
        // If entety is dead
        if (_healthScript.GetCurrentHealth() > 0) return;
        
        GameManager.Instance.AddScore(statSo.scoreAmount);
        GameManager.Instance.AddExperiencePoints(statSo.experienceAmount);
        
        Destroy(gameObject);
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.TakeDamage(statSo.strenght);
        }

    }
    
}
