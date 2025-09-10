
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PrimeTween;

public class PlayerBehaviour : MonoBehaviour
{ 
    public InputSystem_Actions _input;
    
    SpriteRenderer spriteRenderer;

    private bool isInvincible;
    [SerializeField] private float IFrameTime;
    [SerializeField] private Color hitTint = Color.red;
    
    void Start()
    {
        spriteRenderer =  GetComponent<SpriteRenderer>();
        
        _input = new InputSystem_Actions();
        
        _input.Enable();

        _input.HealthDamage._Damage.performed += playerdamae;
        _input.HealthDamage._Healing.performed += playerhealthing;
    }
    
    
    // test functions for damage player and heal player
    private void playerdamae(InputAction.CallbackContext context)
    {
        PlayerTakeDamage(10);
    }
    private void playerhealthing(InputAction.CallbackContext context)
    {
        PlayerHeal(10);
    }
    
    public void PlayerTakeDamage(int damage)
    {
        // if player has iFrames do nothing
        if (isInvincible)  return;
        
        
        GameManager.gameManager.PlayerHealth.TakeDamage(damage);
        
        
        // Debug.Log(GameManager.gameManager.PlayerHealth.CurrentHealth);
        if (GameManager.gameManager.PlayerHealth.CurrentHealth <= 0)
        {
            GameManager.gameManager.GameOver();
        }
        
        StartCoroutine(IFrames());
        
    }
    
    
    void HurtAnimation()
    {
        // Debug.Log("HurtAnimation");
        Sequence.Create()
            .Group(Tween.Color(spriteRenderer, hitTint, 0.1f))
            // .Group(Tween.Scale(transform, new Vector3(transform.localScale.x * 1.2f, 1.2f, 1.2f), 0.1f))
            .ChainDelay(0.5f)
            // .Group(Tween.Scale(transform, new Vector3(transform.localScale.x * 1f, 1f, 1f), IFrameTime))
            .Group(Tween.Color(spriteRenderer, Color.white, IFrameTime));
    }
    
    private static void PlayerHeal(int healing)
    {
        GameManager.gameManager.PlayerHealth.Heal(healing);
    }

    private IEnumerator  IFrames()
    {
        isInvincible = true;
        // Debug.Log("IFrame True");
        HurtAnimation();
        yield return new WaitForSeconds(IFrameTime);
        // Debug.Log("IFrame False");
        isInvincible = false;
    }
    
    

}
