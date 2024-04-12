using UnityEngine;
public struct HitData
{
    public Transform SourceTransform;
    public Transform targetTransform;
    public int damage;
    

    public HitData(Transform sourceTransform, Transform targetTransform, int damage)
    {
        this.SourceTransform = sourceTransform;
        this.targetTransform = targetTransform;
        this.damage = damage;
    }
}