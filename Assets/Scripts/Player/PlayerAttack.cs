using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    // [SerializeField] private Animator animatorHoe;

    // [SerializeField] private GameObject hoe;
    // [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int strenght = 5;
    [SerializeField] private int knockbackForce;
    
    private float _timeUntilMelee;


    void Start()
    {
        // playerMovement = GetComponent<PlayerMovement>();
    }
    
    
    // Another way to switch side to attack is to 
    // Flip Hoe position with * -1 and flip Hoe Graphic on X

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
        if (!collision.gameObject.CompareTag("Enemy")) return;

        collision.GetComponent<EnemyBehaviour>().EnemyTakeDamage(strenght);
        
        
        // Adds knockback hopefully
        Vector2 difference = (transform.position - collision.transform.position).normalized;
        Vector2 force = difference * knockbackForce;
        collision.GetComponent<Rigidbody2D>().AddForce(force,  ForceMode2D.Force);

    }

}
