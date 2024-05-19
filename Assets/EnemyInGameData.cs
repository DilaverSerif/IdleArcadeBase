using System;
using Sirenix.OdinInspector;
[Serializable]
public class EnemyInGameData
{
    [BoxGroup("Health System")]
    public int maxHealth;
    [BoxGroup("Move System")]
    public float moveSpeed;
    [BoxGroup("Move System")]
    public float acceleration;
    
    [BoxGroup("Attack System")]
    public int attackDamage;
    [BoxGroup("Attack System")]
    public float attackSpeed;
    [BoxGroup("Attack System")]
    public float attackRange;
    [BoxGroup("Attack System")]
    public float visionRange;
    [BoxGroup("Attack System")]
    public IDamageable target;
    [BoxGroup("Attack System")]
    public float attackAngle;

}