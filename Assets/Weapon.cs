using UnityEngine;
public abstract class Weapon: MonoBehaviour
{
    public float bulletSpeed = 20;
    public Transform firePoint;
    public ThrowObject bulletPrefab;
    public abstract void Shoot(HitData hitData);
}