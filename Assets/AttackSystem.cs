using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
public abstract class AttackSystem: MonoBehaviour
{
    public TargetFinder<Transform> targetFinder;

    public bool IsTargeting
    {
        get
        {
            return targetFinder.targets.Count > 0;
        }
    }

    public abstract void Attack();

    public Vector3 GetClosestTarget()
    {
        return targetFinder.targets.Count == 0 ? Vector3.zero : targetFinder.targets[0].position;
    }
    
    public Transform GetClosestTargetTransform()
    {
        return targetFinder.targets.Count == 0 ? null : targetFinder.targets[0];
    }
    
    void Update()
    {
        targetFinder?.OnLogic(transform);
    }
    
    void OnDrawGizmosSelected()
    {
        targetFinder?.OnDrawGizmos(transform);
    }
}