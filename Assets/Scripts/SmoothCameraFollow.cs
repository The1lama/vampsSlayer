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
    
    
    [SerializeField] private float maxX, maxY, minX, minY;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float angleInDegrees;
    public Transform target;
    

    private void Start()
    {
        if (target == null)
        {
            Debug.LogWarning($"No target assigned to SmoothCameraFollow, in <color=yellow>{name}</color>");
        }
        
        
    }


    private void FixedUpdate()
    {
        
        // CheckCameraBarrier(target.position);
        
        // if (!_inPlayArea) return;
        
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = transform.position.z; 
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, damping);
    }

    private static Vector2 GetPositionOnCircle(float radius, float angleInDegrees)
    {
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians) * radius;
        float y = Mathf.Sin(angleInRadians) * radius;
        
        return new Vector2(x, y);
    }
    

    private void CheckCameraBarrier(Vector3 player)
    {

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
        // Gizmos.DrawWireSphere(transform.position, 8f);
        // Gizmos.DrawCube(GetPositionOnCircle(spawnRadius, angleInDegrees), Vector3.one);
        Gizmos.DrawLine(new Vector3(minX,maxY,1), new Vector3(maxX,maxY,1));
        Gizmos.DrawLine(new Vector3(maxX,maxY,1), new Vector3(maxX,minY,1));
        Gizmos.DrawLine(new Vector3(maxX,minY,1), new Vector3(minX,minY,1));
        Gizmos.DrawLine(new Vector3(minX,minY,1), new Vector3(minX,maxY,1));
    }
}
