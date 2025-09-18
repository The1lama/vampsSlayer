using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{

    private Animator _animator;
    private Rigidbody2D _rb;
    private InputSystem_Actions _playerControl;
    
    [HideInInspector] public float speed;
    private bool _isfacingRight = true;
    
    private Vector2 _moveDirection = Vector2.zero;
    
    
    public void OnDisable()
    {
        _playerControl.Disable();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        
        _playerControl = new InputSystem_Actions();
        _playerControl.Enable();
        
    }

    void Update()
    {
        _moveDirection =  _playerControl.Player.Move.ReadValue<Vector2>();
        
        _rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * speed;
        
        AnimationStates();
        ChangeDirection(); 
        
    }


    void ChangeDirection()
    {
        if (_moveDirection.x > 0  && !_isfacingRight)  // if facing right
        {
            SpriteChangeDirection();
        } 
        else if (_moveDirection.x < 0 && _isfacingRight)    // if facing left
        {
            SpriteChangeDirection();
        }
    }

    void SpriteChangeDirection()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        _isfacingRight = !_isfacingRight; // makes the var reverse of what it is true
        
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
