using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    [SerializeField] private float normalBulletSpeed;
    [SerializeField] private float destroyTime;
    
    public int strenght;
    
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        SetDestroy();
        SetStraightVelocity();
    }

    private void SetStraightVelocity()
    {
        _rigidbody.linearVelocity = transform.right * normalBulletSpeed;
    }

    private void SetDestroy()
    {
        Destroy(gameObject, destroyTime);
    }
    
    private void OnTriggerEnter2D(Collider2D coll2d)
    {
        Debug.Log($"<Color=blue>{coll2d.gameObject.name}</Color> on TriggerEnter2D");

        if (coll2d.CompareTag("Enemy"))
        {
            coll2d.GetComponent<EnemyBehaviour>().TakeDamage(strenght);
        }
        

    }
    
}
