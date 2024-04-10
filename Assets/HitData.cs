using UnityEngine;
public struct HitData
{
    public Transform targetTransform;
    public int damage;
    
    public HitData(int damage, Transform targetTransform)
    {
        this.damage = damage;
        this.targetTransform = targetTransform;
    }
}