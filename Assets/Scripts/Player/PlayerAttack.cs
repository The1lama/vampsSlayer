using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private float meleeSpeed;
    [SerializeField] private int strenght = 5;
    
    
    private float timeUntilMelee;

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
            animator.SetTrigger("Attack");
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
