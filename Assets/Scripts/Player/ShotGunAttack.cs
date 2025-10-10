using UnityEngine;
using UnityEngine.InputSystem;

public class ShotGunAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] bulletSpawnPoints;

    private float _meleeSpeed;
    private int _strenght;
    private float _timeUntilMelee;
    
    private Vector2 _attackDirection;
    private Vector2 _worldPosition;
    
    [SerializeField] private AudioClip shotgunSound;
    private AudioSource _audioSource;
    
    
    private void Awake()
    {
        GameManager.Instance.onDead.AddListener(() => HideObject());
        
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = shotgunSound;
        
    }
    
    #region Initialize attack settings (functions)
    

    public void SetMeleeSpeed(float newMeleeSpeed)
        {
            _meleeSpeed += newMeleeSpeed;
        }
    
        public void SetNewMeleeSpeed(float newMeleeSpeed)
        {
            _meleeSpeed -= newMeleeSpeed;
            if (_meleeSpeed <= 0)
            {
                _meleeSpeed = 0.2f;
            }
        }
    
        public void SetStrenght(int newStrenght)
        {
            _strenght += newStrenght;
        }

    #endregion
    
    
    private void Update()
    {
        HandleGunRotation();
        
        if (_timeUntilMelee <= 0f)
        {
            PlayerAttack();
            _timeUntilMelee = _meleeSpeed;
        }
        else
        {   
            _timeUntilMelee -= Time.deltaTime;
        }
    }

    private void PlayerAttack()
    {
        foreach (var bulletSpawnPoint in bulletSpawnPoints)
        {
            Vector3 position = bulletSpawnPoint.transform.position;
            
            var bullet = ObjectPoolManager.SpawnObject(bulletPrefab, position, transform.rotation, ObjectPoolManager.PoolType.BulletObject);
            bullet.GetComponent<BulletScript>().strenght = _strenght;
            
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.Play();
            
        }
    }

    private void HandleGunRotation()
    {
        float facingDirection = player.transform.localScale.x;
        
        _worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _attackDirection = (_worldPosition - (Vector2)transform.position).normalized;

        transform.right = _attackDirection; 
        
        var angle = Mathf.Atan2(_attackDirection.y, _attackDirection.x) * Mathf.Rad2Deg;
        
        Vector3 localScale = new Vector3(facingDirection, 1f, 1f);
        if (angle > 90 || angle < -90)

        {
            localScale.y = -1f;
        }
        else 
        {
            localScale.y = 1f;
        }
        transform.localScale = localScale;
    }
    
    private void HideObject()
    {
        gameObject.SetActive(false);
    }
}
