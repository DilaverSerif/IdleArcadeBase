using UnityEngine;
public abstract class Weapon : MonoBehaviour
{
    public float attackCooldown = 0.5f;
    public float bulletSpeed = 20;
    public Transform firePoint;

    public abstract void Shoot(HitData hitData);
    public virtual bool CanShoot(float loadTime) => loadTime >= attackCooldown;

}