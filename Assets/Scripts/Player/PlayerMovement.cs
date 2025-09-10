using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{

    private Animator _animator;
    public Rigidbody2D rb;
    private SpriteRenderer _spriteRenderer;
    
    public InputAction playerControl;
    public float speed = 10f;

    public bool isfacingRight = true;
    
    private Vector2 _moveDirection =  Vector2.zero;


    public void OnEnable()
    {
        playerControl.Enable();
    }

    public void OnDisable()
    {
        playerControl.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _moveDirection =  playerControl.ReadValue<Vector2>();

        rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y).normalized * speed;

        AnimationStates();
        
        ChangeDirection();
        
    }


    void ChangeDirection()
    {
        if (_moveDirection.x > 0 )
        {
             _spriteRenderer.flipX = false;
            //transform.localScale *= new Vector2(1, 1);
            isfacingRight = true;
        } else if (_moveDirection.x < 0)
        {
             _spriteRenderer.flipX = true;
            //transform.localScale *= new Vector2(-1, 1);
            isfacingRight = false;
        }
    }
    
    
    // void Update()
    // {
        // IsMoving();
        // _moveDirection = playerControl.ReadValue<Vector2>();
        // SetFacingDirection(_moveDirection);
        //
        // rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * speed;
        //
        // Debug.Log(_moveDirection);
        //
        // AnimationStates();
    // }
    
    // private void IsMoving()
    // {
    //     _moveDirection = playerControl.ReadValue<Vector2>();
    //     SetFacingDirection(_moveDirection);
    // }
    //
    // private void FixedUpdate()
    // {
    //      IsMoving();
    //     
    //      rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y).normalized * speed;
    //     
    //      Debug.Log(_moveDirection);
    //     
    //      AnimationStates();
    //     
    //      Keyboard kb = InputSystem.GetDevice<Keyboard>();
    //      if (kb.pKey.wasPressedThisFrame)
    //     {
    //         _animator.SetBool("isDead", true);
    //     }
    //     
    // }
    //
    // private void SetFacingDirection(Vector2 moveInput)
    // {
    //     switch (moveInput.x)
    //     {
    //         case > 0 :
    //             isfacingRight = true;
    //             _spriteRenderer.flipX = false;
    //             // transform.localScale *= new Vector2(1, 1);
    //             break;
    //         
    //         case < 0:
    //             isfacingRight = false;
    //             _spriteRenderer.flipX = true;
    //
    //             // transform.localScale *= new Vector2(-1, 1);
    //             break;
    //     }
    // }
    
    private void AnimationStates()
    {
        if (rb.linearVelocity.x == 0 && rb.linearVelocity.y == 0)
        {
            _animator.SetBool("isRuning", false);
        }
        else
        {
            _animator.SetBool("isRuning", true);
        }
    }
    
    
    
    
}
