using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{

    public int strenght;
    public int experience;
    public int scoreAmount;
    public float speed = 6;

    
    private HealthScript _healthScript;
    private Animator _animator;
    
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _healthScript = GetComponent<HealthScript>();

    }
   
    public void EnemyTakeDamage(int damage)
    {

        _healthScript.TakeDamage(damage);
        _animator.SetTrigger("isHurt");
        
        // If entety is dead
        if (_healthScript.GetHealth() > 0) return;
        
        // Debug.Log("Enemy Dead");
        GameManager.gameManager.AddScore(scoreAmount);
        GameManager.gameManager.AddExperiencePoints(experience);
        
        Destroy(gameObject);
        

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBehaviour>().PlayerTakeDamage(strenght);
        }
    }
    
    
    
}
