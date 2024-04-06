using UnityEngine;
public class TestWeapon : Weapon
{
    public override void Shoot(HitData hitData)
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var launchData = new LaunchData
        {
            targetPosition = hitData.targetTransform.position,
            launchPower = bulletSpeed
        };
        
        bullet.Launch(launchData);
    }
}