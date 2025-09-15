
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PrimeTween;

public class PlayerBehaviour : MonoBehaviour
{ 
    InputSystem_Actions _input;
    
    SpriteRenderer _spriteRenderer;

    private bool _isInvincible;
    [SerializeField] private float IFrameTime;
    [SerializeField] private Color hitTint = Color.red;
    
    private HealthScript _healthScript;
    
    public healthBar healthBar;
    
    void Start()
    {
        _spriteRenderer =  GetComponent<SpriteRenderer>();
        _healthScript = GetComponent<HealthScript>();
        
        _input = new InputSystem_Actions();
        
        _input.Enable();

        healthBar.SetMaxHealth(_healthScript.GetHealth());

    }
    
    private void PlayerHeal(int healing)
    {
        _healthScript.Healing(healing);
        healthBar.SetHealth(_healthScript.GetHealth());
    }
    
    
    public void PlayerTakeDamage(int damage)
    {
        // if player has iFrames do nothing
        if (_isInvincible)  return;
        
        
        _healthScript.TakeDamage(damage);
        healthBar.SetHealth(_healthScript.GetHealth());

        // Debug.Log(_healthScript.GetHealth());
        
        if (_healthScript.GetHealth() <= 0)
        {
            // Debug.LogError("Game Over Bitch");
            GameManager.gameManager.GameOver();
        }

        
        StartCoroutine(IFrames());
        
    }
    
    
    void HurtAnimation()
    {
        // Debug.Log("HurtAnimation");
        Sequence.Create()
            .Group(Tween.Color(_spriteRenderer, hitTint, 0.1f))
            // .Group(Tween.Scale(transform, new Vector3(transform.localScale.x * 1.2f, 1.2f, 1.2f), 0.1f))
            .ChainDelay(0.5f)
            
            // .Group(Tween.Scale(transform, new Vector3(transform.localScale.x * 1f, 1f, 1f), IFrameTime))
            .Group(Tween.Color(_spriteRenderer, Color.white, IFrameTime));
    }
    
    
    private IEnumerator  IFrames()
    {
        _isInvincible = true;
        // Debug.Log("IFrame True");
        HurtAnimation();
        yield return new WaitForSeconds(IFrameTime);
        // Debug.Log("IFrame False");
        _isInvincible = false;
    }
    
    

}
