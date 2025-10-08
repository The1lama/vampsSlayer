using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public class PlayerMovement : MonoBehaviour
{

    private Animator _animator;
    private AudioSource _audioSource;
    private Rigidbody2D _rb;
    private InputSystem_Actions _playerControl;

    private bool _isPlaying;
    private float _speed;
    private bool _isFacingRight = true;
    
    private Vector2 _moveDirection = Vector2.zero;
    

    private void OnEnableInput()
    {
        _playerControl.Enable();
    }
    
    private void OnDisableInput()
    {
        _playerControl.Disable();
    }
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        
        _playerControl = new InputSystem_Actions();
        
        OnEnableInput();
        
        GameManager.Instance.onDead?.AddListener( OnDisableInput );
        
    }
    
    
    public void SetSpeed(float newSpeed)
    {
        _speed += newSpeed;
    }
    
    
    void Update()
    {
        _moveDirection =  _playerControl.Player.Move.ReadValue<Vector2>();
        
        _rb.linearVelocity = new Vector2(_moveDirection.x, _moveDirection.y) * _speed;
        
        AnimationStates();
        ChangeDirection();

        if (_rb.linearVelocity.magnitude > 0.1f && !_isPlaying)
        {
            StartCoroutine(PlayWalking());
        }
    }

    IEnumerator PlayWalking()
    {
        _isPlaying = true;
        float randPitch = Random.Range(0.8f, 1.2f);
        _audioSource.pitch = randPitch;
        _audioSource.Play();
        yield return new WaitForSeconds(0.3f);
        _isPlaying = false;
    }


    #region Sprite Changes

        void ChangeDirection()
        {
            if (_moveDirection.x > 0  && !_isFacingRight)  // if facing right
            {
                SpriteChangeDirection();
            } 
            else if (_moveDirection.x < 0 && _isFacingRight)    // if facing left
            {
                SpriteChangeDirection();
            }
        }
    
        void SpriteChangeDirection()
        {
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1;
            transform.localScale = currentScale;
            _isFacingRight = !_isFacingRight; // makes the var reverse of what it is true
            
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
        
    #endregion
    

}
