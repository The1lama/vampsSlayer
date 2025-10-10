using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class XPDrop : MonoBehaviour
{
    [HideInInspector]
    public int xp;
    [HideInInspector]
    public bool canHeal;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource =  GetComponent<AudioSource>();
    }
    
    
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

        _audioSource.Play();
        
        if (canHeal)
        {
            coll.GetComponent<PlayerBehaviour>().Heal(10);
        }

        StartCoroutine(WaitForSound());
    }

    private IEnumerator WaitForSound()
    {
        while (_audioSource.isPlaying)
        {
            yield return !_audioSource.isPlaying;
        }
        
        ObjectPoolManager.ReturnObjectToPool(gameObject, ObjectPoolManager.PoolType.DroppedItems);
    }
    
}
