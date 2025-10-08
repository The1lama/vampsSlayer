using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;

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
        PlayerAttack();
    }

    private void PlayerAttack()
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
        var obj = collision.GetComponent<IDamageable>();
        
        if (obj != null)
        {
            obj.TakeDamage(_strenght);
        }
    }

}
