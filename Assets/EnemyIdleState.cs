using System;
using UnityHFSM;
public class EnemyIdleState: EnemyState
{
    public EnemyIdleState(EnemyBrain theBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null, Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null) : base(theBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        TheBrain.enemyMovement.Stop();
    }
}