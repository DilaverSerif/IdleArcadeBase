using Sirenix.OdinInspector;

public class PlayerAttackSystem : AttackSystem
{
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