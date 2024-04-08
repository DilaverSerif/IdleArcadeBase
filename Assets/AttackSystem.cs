using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
public abstract class AttackSystem : MonoBehaviour
{
    [FormerlySerializedAs("CurrentWeapon")]
    public Weapon currentWeapon;
    public Action<Weapon> ChangedWeapon;
    [ShowInInspector]
    protected float currentAttackCooldown;
    public TargetFinder<Transform> targetFinder;
    
    private UniTask attackTimer;
    private UniTask downAttackTimer;
    
    private CancellationTokenSource downAttackCancellationToken;
    private CancellationTokenSource attackCancellationToken;
    
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
            return currentAttackCooldown > currentWeapon.attackCooldown;
        }
    }
    
    public async virtual UniTask AttackTimer()
    {
        while (targetFinder.targets.Count > 0)
        {
            if (currentAttackCooldown > currentWeapon.attackCooldown)
            {
                await UniTask.Yield();
                continue;
            }
            
            currentAttackCooldown += Time.deltaTime;
            
            await UniTask.WaitForFixedUpdate(cancellationToken:attackCancellationToken.Token);
            if(attackCancellationToken.IsCancellationRequested)
                break;
        }

        downAttackCancellationToken = new CancellationTokenSource();
        downAttackTimer = DownAttackTimer();
    }

    public async virtual UniTask DownAttackTimer()
    {
        while (currentAttackCooldown > 0)
        {
            currentAttackCooldown -= Time.deltaTime;
            await UniTask.WaitForFixedUpdate(cancellationToken:downAttackCancellationToken.Token);
            
            if(downAttackCancellationToken.IsCancellationRequested)
                break;
        }
        
        currentAttackCooldown = 0;
    }

    public virtual void Attack()
    {
        currentAttackCooldown = 0;
    }

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

        AttackTimerStarter();
    }
    private void AttackTimerStarter()
    {
        if(targetFinder.targets.Count > 0 && attackTimer.Status != UniTaskStatus.Pending)
        {
            if(downAttackTimer.Status != UniTaskStatus.Pending)
                downAttackCancellationToken?.Cancel();
            
            attackCancellationToken = new CancellationTokenSource();
            attackTimer = AttackTimer();
        }
    }

    void OnDrawGizmosSelected()
    {
        targetFinder?.OnDrawGizmos(transform);
    }
}