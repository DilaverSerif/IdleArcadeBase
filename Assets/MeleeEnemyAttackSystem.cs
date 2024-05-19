using UnityEngine;
public class MeleeEnemyAttackSystem : AttackSystem
{
    public override bool CanAttack => currentWeapon.CanShoot(currentAttackCooldown);

    public override void Attack()
    {
        var currentMelee = currentWeapon as MeleeWeapon;
        if (currentMelee == null)
        {
            Debug.LogError("No melee weapon");
            return;
        }
        
        var hit = transform.CheckCollision<PlayerHealth>(currentMelee.weaponAttackAngle, currentMelee.weaponAttackRange, 10);
        if (hit == null)
        {
            Debug.LogWarning("No hit maybe missing");
            return;
        }
        
        hit.TakeDamage(new HitData(currentMelee.transform, hit.transform, currentMelee.damage));
    }
}