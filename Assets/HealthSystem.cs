using UnityEngine;
public abstract class HealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public virtual void TakeDamage(HitData hitData)
    {
        if(currentHealth <= 0) return;
        currentHealth = Mathf.Clamp(currentHealth - hitData.damage, 0, maxHealth);
        
        if(currentHealth <= 0)
        {
            Die();
            return;
        }
        
        Hit();
    }
    public virtual void Heal(int heal)
    {
        if(currentHealth >= maxHealth) return;
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
    }
    public abstract void Die();
    public abstract void Revive();
    public abstract void Hit();
}