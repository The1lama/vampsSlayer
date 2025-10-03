using System;
using System.Collections;
using UnityEngine;
using PrimeTween;
using uPools;


public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public EnemyScriptableObject statSo;
    
    [Tooltip("Runtime level Assigned by spawner")]
    private int _level = 1;

    public float _maxHealth;
    private float _strength;
    public float _moveSpeed;
    
    private HealthScript _healthScript;
    private MoveToPlayer _moveToPlayerScript;
    private SpriteRenderer _spriteRenderer;
    

    private void Awake()
    {
        if (statSo == null)
        {
            Debug.LogError("EnemyBehaviour does not have its ScriptableObject");
        }
    }
    
    private void Start()
    {

        _healthScript = GetComponent<HealthScript>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _spriteRenderer.sprite = statSo.enemySprite;
        
        Initialize(_level);
        
        // Starts walk animation
        StartCoroutine(AnimationWalk());
    }

    public void Initialize(int assignedLevel)
    {
        _level = assignedLevel;
        ApplyStats();
    }

    private void OnEnable()
    {
        Initialize(_level);
    }

    private void ApplyStats()
    {
        _maxHealth = StatScaler.ApplyScaling(statSo.baseHealth, _level, statSo, statSo.healthCurve);
        _strength = StatScaler.ApplyScaling(statSo.baseStrenght, _level, statSo, statSo.strenghtCurve);
        _moveSpeed = StatScaler.ApplyScaling(statSo.baseSpeed, _level, statSo, statSo.speedCurve);
        
        
        Debug.Log(Mathf.RoundToInt(_maxHealth));
        Debug.Log(_moveSpeed);
        
        _healthScript.SetMaxHealth(Mathf.RoundToInt(_maxHealth));
        _moveToPlayerScript.SetSpeed(_moveSpeed);
        
    }
    
    
    // private void SetUpEnemy()
    // {
    //     _runtimeStatSo = Instantiate(statSo);
    //     // _runtimeStatSo = _runtimeStatSo.ScaleUpForLevel(enemyLevel);
    //     
    //     _healthScript.SetMaxHealth(_runtimeStatSo.baseHealth);
    //     _moveToPlayerScript.SetSpeed(_runtimeStatSo.baseSpeed);
    //     
    //     _strenght =  _runtimeStatSo.baseStrenght;
    //     
    //     _spriteRenderer.sprite = _runtimeStatSo.enemySprite;
    // }
    
    
    public void TakeDamage(int strength)
    {
        _healthScript.TakeDamage(strength);
        AnimationHurt();
        
        // If entity is dead
        if (_healthScript.GetCurrentHealth() > 0) return;
        
        GameManager.Instance.AddScore(statSo.scoreAmount);
        GameManager.Instance.AddExperiencePoints(statSo.experienceAmount);
        
        ObjectPoolManager.ReturnObjectToPool(gameObject);
        
    }
    
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        var obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.TakeDamage((int)_strength);
        }
    }

    private IEnumerator AnimationWalk()
    {
        Tween.Rotation(transform, endValue: Quaternion.Euler(0, 0, 20), duration: 0.5f);
        yield return new WaitForSeconds(0.5f);
        Tween.Rotation(transform, endValue: Quaternion.Euler(0, 0, -20), duration: 0.5f);
        yield return new WaitForSeconds(0.5f);
    }
    
    private void AnimationHurt()
    {
        Sequence.Create()
            .Group(Tween.Color(_spriteRenderer, statSo.enemyHit, 0.1f))
            .ChainDelay(0.5f)
            .Group(Tween.Color(_spriteRenderer, Color.white, 0.1f));
    }
}
