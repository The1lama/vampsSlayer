using System;
using System.Collections;
using UnityEngine;
using PrimeTween;


public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    private HealthScript _healthScript;
    private MoveToPlayer _moveToPlayerScript;
    private SpriteRenderer _spriteRenderer;
    
    public EnemyScriptableObject statSo;
    
    private bool _isAttacking;
    private bool _isAlive = true;
    
    private void Start()
    {
        _healthScript = GetComponent<HealthScript>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (statSo == null)
        {
            Debug.LogWarning("EnemyBehaviour does not have its ScriptableObject");
        }
        
        _healthScript.SetMaxHealth(statSo.health);
        _moveToPlayerScript.SetSpeed(statSo.speed);
        
        _spriteRenderer.sprite = statSo.enemySprite;

        // Starts walk animation
        StartCoroutine(AnimationWalk());
    }

    public void TakeDamage(int strength)
    {
        _healthScript.TakeDamage(strength);
        AnimationHurt();
        
        // If entety is dead
        if (_healthScript.GetCurrentHealth() > 0) return;


        _isAlive = false;
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

    private IEnumerator AnimationWalk()
    {
        while (_isAlive)
        {
            Tween.Rotation(transform, endValue: Quaternion.Euler(0, 0, 20), duration: 0.5f);
           
            yield return new WaitForSeconds(0.5f);
            
            
            Tween.Rotation(transform, endValue: Quaternion.Euler(0, 0, -20), duration: 0.5f);
            
            yield return new WaitForSeconds(0.5f);
            
        }

    }
    
    private void AnimationHurt()
    {
   
        Sequence.Create()
            .Group(Tween.Color(_spriteRenderer, statSo.enemyHit, 0.1f))
            .ChainDelay(0.5f)
            .Group(Tween.Color(_spriteRenderer, Color.white, 0.1f));
    }
    
}
