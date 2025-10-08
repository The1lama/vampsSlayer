using System;
using UnityEngine;

public class BlurScript : MonoBehaviour
{
    
    private Canvas _canvas;
    
    
    private void Awake()
    {
        
        GameManager.Instance.onDead?.AddListener(UnBlur);
        
    }

    private void Start()
    {
        _canvas = GetComponent<Canvas>();

    }


    private void UnBlur()
    {
        _canvas.enabled = false;
    }

    private void Blur()
    {
        _canvas.enabled = true;
    }


}
