using System.Collections.Generic;
using UnityEngine;
public abstract class AttackSystem: MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();

    public TargetFinder enterTargetFinder;
    public TargetFinder exitTargetFinder;

    public bool IsTargeting
    {
        get
        {
            return targets.Count > 0;
        }
    }
    protected virtual void Awake()
    {
        
    }
    public Vector3 GetClosestTarget()
    {
        return targets[0].transform.position;
    }
}