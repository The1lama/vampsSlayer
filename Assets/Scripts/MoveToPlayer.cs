using UnityEngine;
using UnityEngine.Serialization;

public class MoveToPlayer : MonoBehaviour
{

    [SerializeField] private  Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float speed = 5; 
    
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, speed * Time.deltaTime);
        
        AnimationStates();
    }
    
    private void AnimationStates()
    {
        // Flips sprite direction on X 
        switch ((_player.transform.position -  transform.position).normalized[0])
        {
            case < 0:
                spriteRenderer.flipX = true;
                break;
            case > 0:
                spriteRenderer.flipX = false;
                break;
        }
    }
    
}
