using System;
using UnityHFSM;


[Serializable]
public class EnemyStateMachine: BaseStateMachine<Enum_EnemyState,EnemyStateEventData,EnemyBrain>
{
    public RouteSystem routeSystem;
    
    private EnemyIdleState idleState;
    private EnemyRushTargetState walkState;
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
        attackState = new EnemyAttackState(brain);
        deadState = new EnemyDeadState(brain);
        hurtState = new EnemyHurtState(brain,needsExitTime:true,exitTime:.5f);
        
        routeState = new EnemyRouteState(brain);
        enemyAngryAttack = new EnemyAngryAttackState(brain);

    }
    protected override void OnSetTransitions()
    {
        stateMachine.AddTransition(Enum_EnemyState.Hurt,Enum_EnemyState.AngryAttack);
    }
    protected override void OnSetStates()
    {
        stateMachine.AddState(Enum_EnemyState.Hurt,hurtState);
        stateMachine.AddState(Enum_EnemyState.Idle,idleState);
        stateMachine.AddState(Enum_EnemyState.Walk,walkState);
        stateMachine.AddState(Enum_EnemyState.Attack,attackState);
        stateMachine.AddState(Enum_EnemyState.Dead,deadState);
        
        stateMachine.AddState(Enum_EnemyState.Route,routeState);
        stateMachine.AddState(Enum_EnemyState.AngryAttack,enemyAngryAttack);
    }
    public override Enum_EnemyState GetStartingState()
    {
        return Enum_EnemyState.Route;
    }
}