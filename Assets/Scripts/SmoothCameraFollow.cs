using System;
using Unity.VisualScripting;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    // camera/Player diff (x,y) (9,5) 
    
    
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    private Vector3 _velocity = Vector3.zero;

    private bool _inPlayArea = true;
    // private Collider2D _collider;
    
    
    [SerializeField] private float maxX, maxY, minX, minY;
    
    public Transform target;
    
    private void FixedUpdate()
    {
        
        CheckCameraBarrier(target.position);
        
        if (!_inPlayArea) return;
        
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z; 
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, damping);
    }

    private void CheckCameraBarrier(Vector3 player)
    {
        // if ((transform.position.x > minX && transform.position.x < maxX) &&
        //     (transform.position.y > minY && transform.position.y < maxY))        
            
        if ((player.x > minX && player.x < maxX) && (player.y > minY && player.y < maxY))
        {
            _inPlayArea =  true;
        } else
        {
            _inPlayArea = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minX,maxY,1), new Vector3(maxX,maxY,1));
        Gizmos.DrawLine(new Vector3(maxX,maxY,1), new Vector3(maxX,minY,1));
        Gizmos.DrawLine(new Vector3(maxX,minY,1), new Vector3(minX,minY,1));
        Gizmos.DrawLine(new Vector3(minX,minY,1), new Vector3(minX,maxY,1));
    }
}
