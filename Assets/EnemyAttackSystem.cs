using System;
public class EnemyAttackSystem : AttackSystem
{
    public override bool CanAttack
    {
        get
        {
            return currentWeapon.CanShoot(currentAttackCooldown);
        }
    }
    
    public override void Attack()
    {
        
    }
}