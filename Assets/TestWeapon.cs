using UnityEngine;
public class TestWeapon : RangedWeapon
{
    public override void Shoot(HitData hitData)
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        if(hitData.targetTransform == null) return;

        var launchData = new LaunchData
        {
            LaunchTransform = transform,
            targetPosition = hitData.targetTransform.position,
            launchPower = bulletSpeed
        };
        
        bullet.Launch(launchData);
    }
}