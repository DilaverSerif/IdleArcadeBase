using System;


[Serializable]
public class EnemyStateMachine: BaseStateMachine<Enum_EnemyState,EnemyStateEventData,EnemyBrain>
{
    EnemyIdleState idleState;
    EnemyWalkState walkState;
    EnemyAttackState attackState;
    EnemyDeadState deadState;
    EnemyHurtState hurtState;
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
    }
    protected override void OnSetTransitions()
    {
        
    }
    protected override void OnSetStates()
    {
        
    }
    public override Enum_EnemyState GetStartingState()
    {
        return Enum_EnemyState.Idle;
    }
}