using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[Serializable]
public class TargetFinder<T> where T : Component
{
    public List<T> targets = new List<T>();
    public string targetTag;
    public float enterRadius;
    public float exitRadius;
    
    public virtual void OnLogic(Transform transform)
    {
        targets = AttackSystemExtensions.GetObjectsWithTagInCircularCast<T>(transform.position, targetTag);
    }

    public void OnDrawGizmos(Transform parent)
    {
        #if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(parent.position, Vector3.up, enterRadius);
        
        Handles.color = Color.blue;
        Handles.DrawWireDisc(parent.position, Vector3.up, exitRadius);
  #endif
    }
}