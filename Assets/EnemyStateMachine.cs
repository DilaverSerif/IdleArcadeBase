using System;

[Serializable]
public class EnemyStateMachine: BaseStateMachine<Enum_EnemyState,EnemyStateEventData,EnemyBrain>
{
    public RouteSystem routeSystem;
    
    private EnemyIdleState idleState;
    private EnemyWalkState walkState;
    private EnemyAttackState attackState;
    private EnemyDeadState deadState;
    private EnemyHurtState hurtState;
    private EnemyRouteState routeState;
    
    
    
    public EnemyStateMachine(EnemyBrain brain) : base(brain)
    {
    }
    protected override void OnCreateStates()
    {
        idleState = new EnemyIdleState(brain);
        walkState = new EnemyWalkState(brain);
        attackState = new EnemyAttackState(brain);
        deadState = new EnemyDeadState(brain);
        hurtState = new EnemyHurtState(brain);
        
        routeState = new EnemyRouteState(brain);
    }
    protected override void OnSetTransitions()
    {
        
    }
    protected override void OnSetStates()
    {
        stateMachine.AddState(Enum_EnemyState.Hurt,hurtState);
        stateMachine.AddState(Enum_EnemyState.Idle,idleState);
        stateMachine.AddState(Enum_EnemyState.Walk,walkState);
        stateMachine.AddState(Enum_EnemyState.Attack,attackState);
        stateMachine.AddState(Enum_EnemyState.Dead,deadState);
        
        stateMachine.AddState(Enum_EnemyState.Route,routeState);
    }
    public override Enum_EnemyState GetStartingState()
    {
        return Enum_EnemyState.Route;
    }
}