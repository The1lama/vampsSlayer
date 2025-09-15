using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private InputSystem_Actions _playerControl;
    [SerializeField] private GameObject hoe;
    
    public float speed = 10f;
    public bool isfacingRight = true;
    
    private Vector2 _moveDirection = Vector2.zero;
    
    
    public void OnDisable()
    {
        _playerControl.Disable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        _playerControl = new InputSystem_Actions();
        _playerControl.Enable();
        
    }

    void Update()
    {
        _moveDirection =  _playerControl.Player.Move.ReadValue<Vector2>();
        
        Debug.Log("<color=green>" + transform.position.x + "</color>, <color=blue> "+ transform.position.y + "</color>");
               
        
        _rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * speed;
        
        AnimationStates();
        ChangeDirection(); 
        
    }


    void ChangeDirection()
    {
        if (_moveDirection.x > 0  && !isfacingRight)  // if facing right
        {
            SpriteChangeDirection();
             // _spriteRenderer.flipX = false;
             // weapons.transform.localScale *= -1;
            //transform.localScale *= new Vector2(1, 1);
            // isfacingRight = true;
            // transform.localScale = new Vector3(1, 1, 1);
            
            
        } else if (_moveDirection.x < 0 && isfacingRight)    // if facing left
        {
            SpriteChangeDirection();
            // transform.localScale = new Vector3(-1, 1 , 1);
             // _spriteRenderer.flipX = true;
             // weapons.transform.localScale *= -1;
            //transform.localScale *= new Vector2(-1, 1);
            // isfacingRight = false;
           
            
            
        }
    }

    void SpriteChangeDirection()
    {
        Vector3 currentScale = transform.localScale;
        // Debug.Log("Incoming input: " +currentScale.x);
        currentScale.x *= -1;
        // Debug.Log(currentScale.x);
        // Debug.Log("-------------------------------------");
        transform.localScale = currentScale;
        
        isfacingRight = !isfacingRight; // makes the var reverse of what it is true
        
    }
    
    
    private void AnimationStates()
    {
        if (_rb.linearVelocity.x == 0 && _rb.linearVelocity.y == 0)
        {
            _animator.SetBool("isRuning", false);
        }
        else
        {
            _animator.SetBool("isRuning", true);
        }
    }
    
    
    
    
}
