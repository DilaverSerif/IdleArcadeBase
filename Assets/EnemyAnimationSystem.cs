using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class EnemyAnimationSystem: CharacterSystem<EnemyBrain>
{
    static readonly int Attack = Animator.StringToHash("Attack");
    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int Hurt = Animator.StringToHash("Hurt");
    static readonly int Health = Animator.StringToHash("Health");

    private NavMeshAgent agent;
    private Animator defaultAnimator;

    public EnemyAnimationSystem(EnemyBrain brain) : base(brain)
    {
        defaultAnimator = brain.GetComponentInChildren<Animator>();
        agent = brain.enemyMovement.component;
        
        brain.healthSystem.OnHit += OnHit;
        
        defaultAnimator.SetInteger(Health,brain.healthSystem.currentHealth);
    }
    
    public override void OnDisable()
    {
        brain.healthSystem.OnHit -= OnHit;
    }

    public bool IsAttacking()
    {
        return defaultAnimator.GetBool(Attack);
    }

    private void OnHit(int health)
    {
        defaultAnimator.SetInteger(Health,health);
    }
    
    public override void OnUpdate()
    {
        defaultAnimator.SetFloat(Speed,agent.velocity.magnitude);
    }
    
    public void SetAnimation(Enum_EnemyState state)
    {
        switch (state)
        {
            case Enum_EnemyState.Attack:
                defaultAnimator.SetTrigger(Attack);
                break;
            case Enum_EnemyState.Hurt:
                defaultAnimator.SetTrigger(Hurt);
                break;
            default:
                Debug.LogWarning("No animation for state");
                break;
        }
    }

}