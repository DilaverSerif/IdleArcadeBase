using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using UnityHFSM;
public class EnemyState : State<Enum_EnemyState, EnemyStateEventData>
{
    protected readonly EnemyBrain PlayerBrain;
    protected readonly float ExitTime;
    protected bool RequestedExit;
    
    protected readonly Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter;
    protected readonly Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic;
    protected readonly Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit;
    protected Action<State<Enum_EnemyState, EnemyStateEventData>> onExit;
            
    public EnemyState(
        EnemyBrain playerBrain,
        bool needsExitTime = false,
        float exitTime = 0,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onEnter = null,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onLogic = null,
        Action<State<Enum_EnemyState, EnemyStateEventData>> onExit = null,
        Func<State<Enum_EnemyState, EnemyStateEventData>, bool> canExit = null
    )
    {
        this.PlayerBrain = playerBrain;
        this.onEnter = onEnter;
        this.onLogic = onLogic;
        this.onExit = onExit;
        this.canExit = canExit;
        this.ExitTime = exitTime;
        this.needsExitTime = needsExitTime;
    
        this.timer = new Timer();
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        RequestedExit = false;
        onEnter?.Invoke(this);
    }
    
    public override void OnLogic()
    {
        base.OnLogic();
        if (RequestedExit && timer.Elapsed >= ExitTime)
        {
            fsm.StateCanExit();
        }
    
        onLogic?.Invoke(this);
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