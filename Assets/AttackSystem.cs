using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
public abstract class AttackSystem : MonoBehaviour
{
    public Weapon CurrentWeapon;
    public Action<Weapon> OnChangedWeapon;
    protected float currentAttackCooldown;
    public TargetFinder<Transform> targetFinder;

    public bool IsTargeting
    {
        get
        {
            return targetFinder.targets.Count > 0;
        }
    }
    public bool CanAttack
    {
        get
        {
            return currentAttackCooldown > CurrentWeapon.attackCooldown;
        }
    }

    public async virtual UniTask AttackTimer()
    {
        while (true)
        {
            if (currentAttackCooldown > CurrentWeapon.attackCooldown)
            {
                currentAttackCooldown = 0;
            }
            currentAttackCooldown += Time.deltaTime;
            await UniTask.WaitForFixedUpdate();
        }
    }

    public abstract void Attack();

    public Vector3 GetClosestTarget()
    {
        return targetFinder.targets.Count == 0 ? Vector3.zero : targetFinder.targets[0].position;
    }

    public Transform GetClosestTargetTransform()
    {
        return targetFinder.targets.Count == 0 ? null : targetFinder.targets[0];
    }

    void Update()
    {
        targetFinder?.OnLogic(transform);
    }

    void OnDrawGizmosSelected()
    {
        targetFinder?.OnDrawGizmos(transform);
    }
}