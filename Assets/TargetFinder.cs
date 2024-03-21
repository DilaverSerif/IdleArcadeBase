using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public string targetTag;
    public List<GameObject> targets = new List<GameObject>();
    public float radius = 5f;

    void Awake()
    {
        CreateCollider();
    }
    void CreateCollider()
    {
        var nullTransform = new GameObject().transform;
        nullTransform.name = "TargetFinder";
        nullTransform.parent = transform;
        nullTransform.localPosition = Vector3.zero;
        nullTransform.localRotation = Quaternion.identity;
        
        var sphereCollider = nullTransform.gameObject.AddComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        sphereCollider.radius = radius;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targets.Add(other.gameObject);
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targets.Remove(other.gameObject);
        }
    }
}
