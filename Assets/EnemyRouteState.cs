using System;
using UnityHFSM;
public class EnemyRouteState: EnemyState
{
    private RouteSystem routeSystem;
    public EnemyRouteState(EnemyBrain theBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null, Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null) : base(theBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
        routeSystem = theBrain.enemyStateMachine.routeSystem;
    }

    public override void OnLogic()
    {
        base.OnLogic();
        if (routeSystem == null) return;
        
        var nextPoint = routeSystem.GetNextPosition();
        TheBrain.enemyMovement.Move(nextPoint);
    }


}