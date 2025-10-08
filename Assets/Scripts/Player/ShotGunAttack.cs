using UnityEngine;

public class ShotGunAttack : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    private float _meleeSpeed;
    private int _strenght;
    private float _timeUntilMelee;


    private Vector2 _attackDirection;
    [SerializeField]
    
    

    #region attack settings functions

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
        GameObject bulletspawn = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
    }
    
    
}
