using Sirenix.OdinInspector;

public class PlayerAttackSystem : AttackSystem
{
    public override void Attack()
    {
        base.Attack();
        var target = GetClosestTargetTransform();
        var hitData = new HitData
        {
            SourceTransform = transform,
            targetTransform = target,
            damage = 10
        };
        
        currentWeapon.Shoot(hitData);
    }
    void OnDrawGizmosSelected()
    {
        targetFinder.OnDrawGizmos(transform);
    }


    #if UNITY_EDITOR
    [Button]
    public void TestAttack()
    {
        Attack();
    }
  #endif
}