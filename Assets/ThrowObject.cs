using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private Rigidbody rb;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Launch(LaunchData launchData)
    {
        var targetObjectTf = launchData.targetPosition;
        var direction = targetObjectTf - transform.position;
        direction.y += Random.Range(-1f,1f);
        
        rb.AddForce(direction.normalized * launchData.launchPower, ForceMode.Impulse);
    }
}