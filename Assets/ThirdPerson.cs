using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0f, 1.5f, -5f); 

    void LateUpdate()
    {
        
        transform.position = target.position + offset;
        transform.LookAt(target.position);
    }
}
