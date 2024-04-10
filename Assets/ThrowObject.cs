using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            var healthSystem = other.gameObject.GetComponent<EnemyHealth>();
            healthSystem.TakeDamage(new HitData
            {
                damage = 10
            });
        }
    }
}