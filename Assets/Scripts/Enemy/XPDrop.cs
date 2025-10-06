using System;
using UnityEngine;

public class XPDrop : MonoBehaviour
{
    [HideInInspector]
    public int xp;
    
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (!coll.gameObject.CompareTag("Player")) return;
        
        Debug.Log("Player collided with XP");
        
        Debug.Log(xp);
        
        GameManager.Instance.AddExperiencePoints(xp);
        
        Destroy(gameObject);
        
    }
}
