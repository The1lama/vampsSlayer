using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;

    [HideInInspector] public float meleeSpeed;
    [HideInInspector] public int strenght;
    
    private float _timeUntilMelee;
    
    private void Update()
    {
        playerAttack();
    }

    private void playerAttack()
    {
        if (_timeUntilMelee <= 0f)
        {
            animatorPlayer.SetTrigger("Attack");
            _timeUntilMelee = meleeSpeed;
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
            obj.TakeDamage(strenght);
        }
        
    }

}
