using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrowObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform launchParenTransform; //Burayı mantıklı bir hale getirebilir
        //TODO: Struct yapıları güncelle mantıklı bişiler deneyelim
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    public void Launch(LaunchData launchData)
    {
        launchParenTransform = launchData.LaunchTransform;
        
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
                SourceTransform = launchParenTransform,
                targetTransform = other.transform,
                damage = 10
            });
        }
    }
}