using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private AudioClip swingSound;
    private AudioSource _audioSource;

    
    private float _meleeSpeed;
    private int _strenght;
    
    private float _timeUntilMelee;

    private void Awake()
    {
        GameManager.Instance.onDead.AddListener(() => HideObject());
        
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = swingSound;
    }
    
    
    #region Initialize Settings

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

    #endregion
    
    
    private void Update()
    {
        PlayerAttack();
    }

    private void PlayerAttack()
    {
        if (_timeUntilMelee <= 0f)
        {
            animatorPlayer.SetTrigger("Attack");
            
            _audioSource.pitch = Random.Range(0.9f, 1.2f);
            _audioSource.Play();
            
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

    private void HideObject()
    {
        gameObject.SetActive(false);
    }
    

}
