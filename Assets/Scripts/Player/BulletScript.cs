using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int strenght;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float destroyTime;
    private float _currentDestroyTime;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        InitilizeBullet();
    }
    
    private void OnEnable()
    {
        InitilizeBullet();
    }
    
    private void InitilizeBullet()
    {
        _currentDestroyTime =  destroyTime;
        
        SetStraightVelocity();
    }
    
    private void SetStraightVelocity()
    {
        _rigidbody.linearVelocity = transform.right * normalBulletSpeed;
    }

    private void Update()
    {
        SetDestroy();
    }

    private void SetDestroy()
    {
        _currentDestroyTime -= Time.deltaTime;
        if (_currentDestroyTime <= 0f)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.BulletObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D coll2d)
    {
        // Debug.Log($"<Color=blue>{coll2d.gameObject.name}</Color> on TriggerEnter2D");

        if (coll2d.CompareTag("Enemy") && coll2d.GetComponent<IDamageable>() != null)
        {
            coll2d.GetComponent<EnemyBehaviour>().TakeDamage(strenght);
            ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.BulletObject);
        }
        
    }
    
}
