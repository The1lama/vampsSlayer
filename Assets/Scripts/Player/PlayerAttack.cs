using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animatorPlayer;
    // [SerializeField] private Animator animatorHoe;

    // [SerializeField] private GameObject hoe;
    // [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int strenght = 5;
    
    
    private float timeUntilMelee;


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
        if (timeUntilMelee <= 0f)
        {
            animatorPlayer.SetTrigger("Attack");
            timeUntilMelee = meleeSpeed;
        }
        else
        {
            timeUntilMelee -= Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        collision.GetComponent<EnemyBehaviour>().EnemyTakeDamage(strenght);
        
    }

}
