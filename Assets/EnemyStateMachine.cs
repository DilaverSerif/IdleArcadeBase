using System;
using UnityEngine;
using UnityHFSM;


[Serializable]
public class EnemyStateMachine : BaseStateMachine<Enum_EnemyState, EnemyStateEventData, EnemyBrain>
{
    public RouteSystem routeSystem;

    private EnemyIdleState idleState;
    private EnemyRushTargetState walkState;
    private EnemyWalkToTargetState walkToTargetState;
    private EnemyAttackState attackState;
    
    private EnemyDeadState deadState;
    private EnemyHurtState hurtState;
    private EnemyRouteState routeState;
    private EnemyAngryAttackState enemyAngryAttack;



    public EnemyStateMachine(EnemyBrain brain) : base(brain)
    {
    }
    protected override void OnCreateStates()
    {
        idleState = new EnemyIdleState(brain);
        walkState = new EnemyRushTargetState(brain);
        walkToTargetState = new EnemyWalkToTargetState(brain);
        deadState = new EnemyDeadState(brain);
        hurtState = new EnemyHurtState(brain, needsExitTime: true, exitTime: .5f);
        attackState = new EnemyAttackState(brain);
        
        routeState = new EnemyRouteState(brain);
        enemyAngryAttack = new EnemyAngryAttackState(brain, needsExitTime: true);
    }
    protected override void OnSetTransitions()
    {
        stateMachine.AddTransition(Enum_EnemyState.Hurt, Enum_EnemyState.AngryAttack);

        stateMachine.AddTransition(Enum_EnemyState.AngryAttack, Enum_EnemyState.Idle, AngryAttackToIdle);
        stateMachine.AddTransition(Enum_EnemyState.AngryAttack, Enum_EnemyState.RushTarget, AngryAttackToRushTarget);

        stateMachine.AddTransition(Enum_EnemyState.Idle, Enum_EnemyState.Route, IdleToRoute);
        stateMachine.AddTransition(Enum_EnemyState.Idle, Enum_EnemyState.RushTarget,IdleToRushTarget);
        
        stateMachine.AddTransitionFromAny(Enum_EnemyState.RushTarget, ToRush);
        stateMachine.AddTransition(Enum_EnemyState.RushTarget, Enum_EnemyState.Attack,RushToAttack);
        
        stateMachine.AddTransition(Enum_EnemyState.Attack, Enum_EnemyState.Idle,AttackToIdle);
        
        stateMachine.AddTransition(Enum_EnemyState.RushTarget, Enum_EnemyState.Idle, RustAttackToIdle);
    }
    bool IdleToRushTarget(Transition<Enum_EnemyState> arg)
    {
        return brain.attackSystem.IsTargeting && !brain.attackSystem.CanAttack;
    }
    bool AttackToIdle(Transition<Enum_EnemyState> arg)
    {
        return !brain.attackSystem.IsTargeting;
    }
    bool RushToAttack(Transition<Enum_EnemyState> arg)
    {
        return brain.attackSystem.CanAttack;
    }
    bool RustAttackToIdle(Transition<Enum_EnemyState> arg)
    {
        return brain.attackSystem.IsTargeting == false;
    }
    bool ToRush(Transition<Enum_EnemyState> arg)
    {
        return brain.attackSystem.IsTargeting && stateMachine.ActiveStateName != Enum_EnemyState.Idle;
    }

    private bool IdleToRoute(Transition<Enum_EnemyState> arg)
    {
        return !brain.attackSystem.IsTargeting;
    }

    private bool AngryAttackToRushTarget(Transition<Enum_EnemyState> arg)
    {
        return brain.attackSystem.IsTargeting;
    }

    private bool AngryAttackToIdle(Transition<Enum_EnemyState> arg)
    {
        return !brain.attackSystem.IsTargeting;
    }

    protected override void OnSetStates()
    {
        stateMachine.AddState(Enum_EnemyState.Dead, deadState);
        stateMachine.AddState(Enum_EnemyState.Hurt, hurtState);
        
        stateMachine.AddState(Enum_EnemyState.Idle, idleState);
        stateMachine.AddState(Enum_EnemyState.Walk, walkState);
        stateMachine.AddState(Enum_EnemyState.Route, routeState);

        
        stateMachine.AddState(Enum_EnemyState.RushTarget, walkToTargetState);
        stateMachine.AddState(Enum_EnemyState.AngryAttack, enemyAngryAttack);
    }
    public override Enum_EnemyState GetStartingState()
    {
        return Enum_EnemyState.Route;
    }
}