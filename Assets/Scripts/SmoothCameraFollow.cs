using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{

    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    private Vector3 velocity = Vector3.zero;
    
    public Transform Target;
    
    private void FixedUpdate()
    {
        Vector3 targetPosition = Target.position + offset;
        targetPosition.z = transform.position.z; 
        
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);
    }
}
