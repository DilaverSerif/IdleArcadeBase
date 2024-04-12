using System;
using Lean.Common;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public struct LastHitData
{
    public Transform sourceTarget;

    public void Setup(HitData hitData)
    {
        sourceTarget = hitData.SourceTransform;
    }
}

public abstract class HealthSystem<TBrain> : MonoBehaviour where TBrain : class
{
    protected TBrain brain;
    public int maxHealth;
    public int currentHealth;

    public LastHitData lastHitData;


    public void InitializeHealthSystem(TBrain brain, int maxHealth)
    {
        this.brain = brain;
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(HitData hitData)
    {
        if (currentHealth <= 0) return;
        lastHitData = new LastHitData
        {
            sourceTarget = hitData.SourceTransform
        };
        currentHealth = Mathf.Clamp(currentHealth - hitData.damage, 0, maxHealth);

        HitEffect(hitData);

        if (currentHealth <= 0)
        {
            Die();
            return;
        }

        Hit();
    }
    protected virtual void HitEffect(HitData hitData) //Her hasar aldığında çalışır
    {

        var offset = new Vector3(0, 1 + Random.Range(1, 2), Random.Range(1, 2));
        var floatingText = LeanPool.Spawn(GameObjectRepository.Instance.floatingTextPrefab, transform.position + offset, Quaternion.identity);
        floatingText.Initialize(new FloatingTextData
        {
            text = hitData.damage.ToString(),
            color = Color.red
        });
    }
    public virtual void Heal(int heal)
    {
        if (currentHealth >= maxHealth) return;
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
    }
    public abstract void Die();
    public abstract void Revive();
    public abstract void Hit();
    public abstract WarSide GetWarSide();


    #if UNITY_EDITOR
    //Test Area
    [Button]
    public void Kill()
    {
        var health = this.currentHealth;
        TakeDamage(new HitData(transform,null,health));
    }
    
  #endif
}