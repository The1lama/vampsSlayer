using System;
using UnityEngine;

public class DieCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        ObjectPoolManager.ReturnObjectToPool(other.gameObject);
    }
}
