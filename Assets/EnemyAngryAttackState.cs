using System;
using UnityEngine;
using UnityHFSM;

public class EnemyAngryAttackState : EnemyState
{
    private Vector3 lastHitPoint; //gelen hasarın geldigi kaynak vector3

    public EnemyAngryAttackState(EnemyBrain theBrain, bool needsExitTime = false, float exitTime = 0,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null,
        Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null) : base(theBrain, needsExitTime,
        exitTime, onEnter, onLogic, onExit, canExit)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        lastHitPoint = TheBrain.healthSystem.lastHitData.sourceTarget.position;

        if (lastHitPoint != Vector3.zero)
        {
            TheBrain.enemyMovement.Move(lastHitPoint);
        }
    }

    public override void OnExitRequest()
    {
        if (!needsExitTime || canExit != null && canExit(null))
        {
            fsm.StateCanExit();
        }
        else
        {
            RequestedExit = true;
        }
    }
}