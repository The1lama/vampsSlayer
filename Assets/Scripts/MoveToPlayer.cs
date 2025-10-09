using UnityEngine;
using UnityEngine.Serialization;

public class MoveToPlayer : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    private float _speed = 5;
    
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
        if (_player == null)
        {
            _player = null;
            Debug.Log("No player found");
        }
        
        _spriteRenderer =  GetComponent<SpriteRenderer>();
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
    
    
    void Update()
    {
        if (_player != null)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            AnimationStates();
        }
        else
        {
            Vector3 moveTo = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position,moveTo , _speed * Time.deltaTime);
        }
    }
    
    
    
    private void AnimationStates()
    {
        // Flips sprite direction on X 
        switch ((_player.transform.position -  transform.position).normalized[0])
        {
            case < 0:
                _spriteRenderer.flipX = true;
                break;
            case > 0:
                _spriteRenderer.flipX = false;
                break;
        }
    }
    
}
