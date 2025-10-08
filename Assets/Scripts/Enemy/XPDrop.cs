using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class XPDrop : MonoBehaviour
{
    [HideInInspector]
    public int xp;
    [HideInInspector]
    public bool canHeal;


    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        canHeal = false;
    }
    
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("Player")) return;
        
        GameManager.Instance.AddExperiencePoints(xp);

        if (canHeal)
        {
            coll.GetComponent<PlayerBehaviour>().Heal(10);
        }
        
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.DroppedItems);
        
    }
}
