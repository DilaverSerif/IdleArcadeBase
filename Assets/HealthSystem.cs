using UnityEngine;
public abstract class HealthSystem<TBrain> : MonoBehaviour where TBrain: class
{
    protected TBrain brain;
    public int maxHealth;
    public int currentHealth;

    public void InitializeHealthSystem(TBrain brain,int maxHealth)
    {
        this.brain = brain;
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }
    
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
    public abstract WarSide GetWarSide();
}