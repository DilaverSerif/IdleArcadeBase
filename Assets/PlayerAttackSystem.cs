using System;
using Sirenix.OdinInspector;

public class PlayerAttackSystem : AttackSystem
{
    public Weapon CurrentWeapon;
    public Action<Weapon> OnChangedWeapon;
    void OnDrawGizmosSelected()
    {
        targetFinder.OnDrawGizmos(transform);
    }
    public override void Attack()
    {
        var target = GetClosestTargetTransform();
        var hitData = new HitData
        {
            targetTransform = target,
            damage = 10
        };
        
        CurrentWeapon.Shoot(hitData);
    }

    #if UNITY_EDITOR
    [Button]
    public void TestAttack()
    {
        Attack();
    }
  #endif
}