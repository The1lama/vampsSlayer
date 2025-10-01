using System;
using System.Collections;
using UnityEngine;
using PrimeTween;
using uPools;


public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    private HealthScript _healthScript;
    private MoveToPlayer _moveToPlayerScript;
    private SpriteRenderer _spriteRenderer;
    
    public EnemyScriptableObject statSo;
    private EnemyScriptableObject _runtimeStatSo;
    
    private GameObjectPool _pool;


    private void Awake()
    {
        if (statSo == null)
        {
            Debug.LogWarning("EnemyBehaviour does not have its ScriptableObject");
            return;
        }
        _runtimeStatSo = Instantiate(statSo);
        if (_runtimeStatSo == null)
        {
            Debug.LogWarning("EnemyBehaviour does not have its _runtimeStatSo");

        }
    }

    private void OnDisable()
    {
        Debug.Log($"Enemy disabled. runtimeSO is still: {_runtimeStatSo}");
    }
    
    private void Start()
    {

        _healthScript = GetComponent<HealthScript>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        
        _healthScript.SetMaxHealth(_runtimeStatSo.health);
        _moveToPlayerScript.SetSpeed(_runtimeStatSo.speed);
        
        _spriteRenderer.sprite = _runtimeStatSo.enemySprite;

        // Starts walk animation
        StartCoroutine(AnimationWalk());
    }

    public void LevelUpEnemy()
    {
        if(_runtimeStatSo == null)
            Debug.Log("Getting a run time exemption");
        else
        {
            Debug.Log("not fdsfsdffsfs sf sf asap a a run time exemption");
        }
        
        // _runtimeStatSo.LevelUp();
        // Debug.Log($"{name} Level up to {_runtimeStatSo.level}");
    }
    
    
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
            obj.TakeDamage(statSo.strenght);
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
