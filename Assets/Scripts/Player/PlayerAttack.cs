using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    // [SerializeField] private Collider2D AttackBox;

    private float _meleeSpeed;
    private int _strenght;
    
    private float _timeUntilMelee;
    

    public void SetMeleeSpeed(float newMeleeSpeed)
    {
        _meleeSpeed += newMeleeSpeed;
    }

    public void SetNewMeleeSpeed(float newMeleeSpeed)
    {
        _meleeSpeed -= newMeleeSpeed;
        if (_meleeSpeed <= 0)
        {
            _meleeSpeed = 0.2f;
        }
    }

    public float GetMeleeSpeed()
    {
        return _meleeSpeed;
    }

    public void SetStrenght(int newStrenght)
    {
        _strenght += newStrenght;
    }
    
    private void Update()
    {
        playerAttack();
    }

    private void playerAttack()
    {
        if (_timeUntilMelee <= 0f)
        {
            animatorPlayer.SetTrigger("Attack");
            _timeUntilMelee = _meleeSpeed;
        }
        else
        {   
            _timeUntilMelee -= Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log(collision.gameObject.name);
        // Debug.Log("Something hit");
        var obj = collision.GetComponent<IDamageable>();
        
        if (obj != null)
        {
            obj.TakeDamage(_strenght);
        }
        
    }

}
