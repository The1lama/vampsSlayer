using System.Collections;
using UnityEngine;
using PrimeTween;


public class EnemyBehaviour : MonoBehaviour, IDamageable
{

    #region Stats and Drops

        public EnemyScriptableObject statSo;
        public GameObject xpDrop;
        public int playerHealChange;
        
    #endregion

    #region Struc

        [Header("See run time value")]
        
        [Tooltip("Runtime level Assigned by spawner")]
        private int _level = 1;
        private float _maxHealth;
        private float _strength;
        private float _moveSpeed;
        public float spawnDelay;
        
    #endregion
    
    #region Componenets

        private HealthScript _healthScript;
        private MoveToPlayer _moveToPlayerScript;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;

    #endregion
    

    private void Awake()
    {
        if (statSo == null)
        {
            Debug.LogError("EnemyBehaviour does not have its ScriptableObject");
        }
        
        _healthScript = GetComponent<HealthScript>();
        _moveToPlayerScript = GetComponent<MoveToPlayer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }
     
    public void Initialize(int assignedLevel)
    {
        spawnDelay = statSo.spawnTime;
        _spriteRenderer.sprite = statSo.enemySprite;
        
        _level = assignedLevel;
        
        ApplyStats();
        StartCoroutine(AnimationWalk());

    }

    private void ApplyStats()
    {
        
        _maxHealth = StatScaler.ApplyScaling(statSo.baseHealth, _level, statSo, statSo.curve);
        _strength = StatScaler.ApplyScaling(statSo.baseStrenght, _level, statSo, statSo.curve);
        _moveSpeed = StatScaler.ApplyScaling(statSo.baseSpeed, _level, statSo, statSo.curve);
        
        _healthScript.SetMaxHealth(Mathf.RoundToInt(_maxHealth));
        _moveToPlayerScript.SetSpeed(_moveSpeed);
        
    }
    
    
    public void TakeDamage(int strength)
    {
        // Debug.Log("Enemy Take damage");
        _healthScript.TakeDamage(strength);
        AnimationHurt();
        
        // If entity is dead
        if (_healthScript.GetCurrentHealth() > 0) return;
        
        
        // dropped XP
        if (xpDrop != null)
        {
            // var dropped = Instantiate(xpDrop, transform.position, Quaternion.identity);
            var dropped = ObjectPoolManager.SpawnObject(xpDrop,transform.position,Quaternion.identity, ObjectPoolManager.PoolType.DroppedItems);
            dropped.GetComponent<XPDrop>().xp = statSo.experienceAmount;
            if (Random.Range(0, 100) < playerHealChange)
            {
                dropped.GetComponent<XPDrop>().canHeal = true;
            }
        }

        GameManager.Instance.AddScore(statSo.scoreAmount);

        
        // Destroy(gameObject);
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
        while (isActiveAndEnabled)
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
