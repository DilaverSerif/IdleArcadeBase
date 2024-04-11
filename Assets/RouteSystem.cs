using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
[Serializable]
public class RouteSystem
{
    public float waitTime;
    public Vector3[] worldPosition;
    
    [ShowInInspector]
    private float timer;
    
    [ShowInInspector]
    private int currentIndex;

    public Vector3 GetNextPosition()
    {
        timer += Time.deltaTime;
        if (!(timer >= waitTime)) return worldPosition[currentIndex];
        
        timer = 0;
        currentIndex++;
            
        if(currentIndex >= worldPosition.Length)
            currentIndex = 0;
            
        return worldPosition[currentIndex];
    }
    
    public bool CheckDistance(Vector3 position, float targetDistance = 0.5f)
    {
        return Vector3.Distance(worldPosition[currentIndex], position) < targetDistance;
    }

    public void OnGizmos()
    {
        #if UNITY_EDITOR
        if(worldPosition == null) return;
        foreach (var pos in worldPosition)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(pos, 0.5f);
            Gizmos.DrawLine(pos, worldPosition[(Array.IndexOf(worldPosition, pos) + 1) % worldPosition.Length]);
            
            Handles.Label(pos + Vector3.up, $"Route: {Array.IndexOf(worldPosition, pos)}");
        }
  #endif
    }
}