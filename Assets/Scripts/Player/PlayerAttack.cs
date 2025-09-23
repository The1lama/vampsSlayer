using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;

    private float _meleeSpeed;
    private int _strenght;
    
    private float _timeUntilMelee;


    public void SetMeleeSpeed(float newMeleeSpeed)
    {
        _meleeSpeed = newMeleeSpeed;
    }

    public void SetStrenght(int newStrenght)
    {
        _strenght = newStrenght;
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
        
        var obj = collision.GetComponent<IDamageable>();
        if (obj != null)
        {
            obj.TakeDamage(_strenght);
        }
        
    }

}
