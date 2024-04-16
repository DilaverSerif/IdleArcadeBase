using System;
using UnityEngine;
using UnityEngine.AI;


[Serializable]
public class EnemyAnimationSystem: CharacterSystem<EnemyBrain>
{
    static readonly int Attack = Animator.StringToHash("Attack");
    static readonly int Speed = Animator.StringToHash("Speed");

    NavMeshAgent agent;
    
    public EnemyAnimationSystem(EnemyBrain brain) : base(brain)
    {
        defaultAnimator = brain.GetComponentInChildren<Animator>();
        agent = base.brain.enemyMovement.component;
    }
    
    public Animator defaultAnimator;

    public bool IsAttacking()
    {
        return defaultAnimator.GetBool(Attack);
    }

    public override void OnUpdate()
    {
        defaultAnimator.SetFloat(Speed,agent.velocity.magnitude);
    }
    
    public void SetAnimation(Enum_EnemyState state)
    {
        switch (state)
        {

            case Enum_EnemyState.None:
                break;
            case Enum_EnemyState.Idle:
                break;
            case Enum_EnemyState.Walk:
                break;
            case Enum_EnemyState.Attack:
                break;
            case Enum_EnemyState.Hurt:
                break;
            case Enum_EnemyState.Dead:
                break;
            case Enum_EnemyState.Route:
                break;
            case Enum_EnemyState.AngryAttack:
                break;
            case Enum_EnemyState.RushTarget:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

}