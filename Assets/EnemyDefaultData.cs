using System;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDefaultData", menuName = "Enemy/EnemyDefaultData", order = 0)]
public class EnemyDefaultData : ScriptableObject
{
    [BoxGroup("General")]
    public WarSide warSide;
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
}