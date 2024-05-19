using System;
using UnityHFSM;
public class EnemyRushTargetState: EnemyState
{
    private readonly MeleeEnemyAttackSystem meleeEnemyAttackSystem;

    public EnemyRushTargetState(EnemyBrain theBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null, Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null, Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null) : base(theBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
        meleeEnemyAttackSystem = theBrain.attackSystem;
    }

    public override void OnLogic()
    {
        base.OnLogic();

        if (meleeEnemyAttackSystem.IsTargeting)
        {
            TheBrain.enemyMovement.Move(meleeEnemyAttackSystem.GetClosestTarget());
        }
    }
}