using System;
using UnityHFSM;
public class EnemyRushTargetState: EnemyState
{
    private EnemyAttackSystem enemyAttackSystem;

    public EnemyRushTargetState(EnemyBrain theBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null, Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null) : base(theBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
        enemyAttackSystem = theBrain.attackSystem;
    }

    public override void OnLogic()
    {
        base.OnLogic();

        if (enemyAttackSystem.IsTargeting)
        {
            TheBrain.enemyMovement.Move(enemyAttackSystem.GetClosestTarget());
        }
    }
}