using System;
using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    // public int strength;
    // public int experience;
    // public int scoreAmount;
    // public float speed = 6;

    private Rigidbody2D _rb;
    private HealthScript _healthScript;
    private MoveToPlayer _moveToPlayerScript;
    private Animator _animator;
    private PlayerBehaviour _player;
    
    
    public EnemyScriptableObject statSo;
    
    // private float _waitAttackTime = 2;
    private bool _isAttacking;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthScript = GetComponent<HealthScript>();
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerBehaviour>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();

        
        
        _healthScript.SetMaxHealth(statSo.health);
        _moveToPlayerScript.SetSpeed(statSo.speed);


        // StartCoroutine(AttackTime());

    }

    void Update()
    {
        // if (!_isAttacking) return;


        // StartCoroutine(AttackTime());
        // _player.PlayerTakeDamage(10);
        
    }
    
    public void EnemyTakeDamage(int damage)
    {

        _healthScript.TakeDamage(damage);
        _animator.SetTrigger("isHurt");
        
        // If entety is dead
        if (_healthScript.GetCurrentHealth() > 0) return;
        
        // Debug.Log("Enemy Dead");
        GameManager.Instance.AddScore(statSo.scoreAmount);
        GameManager.Instance.AddExperiencePoints(statSo.experienceAmount);
        
        Destroy(gameObject);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().PlayerTakeDamage(statSo.strenght);
        }
    }
    
    //
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Collision Enter");
    //         _isAttacking = true; 
    //         // collision.collider.GetComponent<PlayerBehaviour>().PlayerTakeDamage(strength);
    //     }    
    // }
    //
    // private void OnCollisionExit2D(Collision2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("Collision Exit");
    //         _isAttacking = false;
    //     }
    // }
    //
    // private IEnumerator  AttackTime()
    // {
    //     if (_isAttacking)
    //     {
    //         _player.PlayerTakeDamage(10);
    //     } 
    //     yield return new WaitForSeconds(_waitAttackTime);
    // }
    
}
