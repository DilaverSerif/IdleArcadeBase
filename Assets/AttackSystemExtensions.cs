using System;
using System.Collections.Generic;
using UnityEngine;
public static class AttackSystemExtensions
{
    public static T CheckCollision<T>(this Transform origin, float angle, float length, int rayCount, LayerMask layerMask) where T : MonoBehaviour
    {
        for (int i = 0; i < rayCount; i++)
        {
            float lerpFactor = i / (float)(rayCount - 1);
            float currentAngle = Mathf.Lerp(angle, -angle, lerpFactor);

            var direction = Quaternion.Euler(0, currentAngle, 0) * origin.forward;

            RaycastHit hit;
            if (Physics.Raycast(origin.position, direction, out hit, length, layerMask))
            {
                #if UNITY_EDITOR
                Debug.DrawRay(origin.position, direction * length, Color.yellow, 1);
                #endif
                if (hit.transform.TryGetComponent(out T component))
                    return component;
                else Debug.LogError($"Component {typeof(T)} not found on {origin.name}");
            }
            #if UNITY_EDITOR
            else
            {
                Debug.DrawRay(origin.position, direction * length, Color.red, 1);
            }
            #endif
        }

        return null;
    }
    
    public static Vector3 Evaluate(this Vector3 start, Vector3 end, float height, float t)
    {
        var ab = Vector3.Lerp(start, end, t);
        var ba = Vector3.Lerp(end, start, t);
        var result = Vector3.Lerp(ab, ba, t);
        var distance = Vector3.Distance(start, end);
        distance = distance > 1 ? distance : 1;
        
        return new Vector3(result.x, result.y + Mathf.Sin(t * Mathf.PI) * height, result.z);
    }

    public static List<T> GetObjectsWithTagInCircularCast<T>(Vector3 center, string tag, float radius = 5f, TargetFinderFindType findType = TargetFinderFindType.Closest, int maxColliders = 3) where T : Component
    {
        var objectsWithTag = new List<T>();
        var colliders = new Collider[maxColliders];
        int count = Physics.OverlapSphereNonAlloc(center, radius, colliders);
        
        for (int i = 0; i < count; i++)
        {
            if (!colliders[i].CompareTag(tag)) continue;
            if (colliders[i].TryGetComponent(out T component))
                objectsWithTag.Add(component);
        }
        
        if(objectsWithTag.Count < 1) return objectsWithTag;
        
        switch (findType)
        {
            case TargetFinderFindType.Closest:
                Array.Sort(colliders, (x, y) => Vector3.Distance(center, x.transform.position).CompareTo(Vector3.Distance(center, y.transform.position)));
                break;
            case TargetFinderFindType.Farthest:
                Array.Sort(colliders, (x, y) => Vector3.Distance(center, y.transform.position).CompareTo(Vector3.Distance(center, x.transform.position)));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(findType), findType, null);
        }
        
        return objectsWithTag;
    }
}

public enum TargetFinderFindType
{
    Closest,
    Farthest
}