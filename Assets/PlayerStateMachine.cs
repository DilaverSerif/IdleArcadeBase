using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using Sirenix.OdinInspector;
using UnityHFSM;
[Serializable]
public class PlayerStateMachine
{
    private StateMachine<Enum_PlayerState, PlayerStateEventData> stateMachine;
    public StateMachine<Enum_PlayerState, PlayerStateEventData> StateMachine
    {
        get
        {
            return stateMachine;
        }
    }

    [ShowInInspector]
    public Enum_PlayerState CurrentState
    {
        get
        {
            if (stateMachine == null)
                return Enum_PlayerState.None;
            return stateMachine.ActiveStateName;
        }
    }

    private PlayerBrain playerBrain;

    PlayerIdleState idleState;
    PlayerWalkState walkState;
    PlayerTargetState targetState;
    public PlayerStateMachine(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;
        stateMachine = new StateMachine<Enum_PlayerState, PlayerStateEventData>();

        idleState = new PlayerIdleState(playerBrain);
        walkState = new PlayerWalkState(playerBrain);
        targetState = new PlayerTargetState(playerBrain);

        OnSetStates();
    }

    private void OnSetStates()
    {
        stateMachine.AddState(Enum_PlayerState.Idle, idleState);
        stateMachine.AddState(Enum_PlayerState.Walk, walkState);
        stateMachine.AddState(Enum_PlayerState.Targeting, targetState);

        stateMachine.AddTransition(Enum_PlayerState.Idle,Enum_PlayerState.Walk, _ => BeWalking());
        stateMachine.AddTransition(Enum_PlayerState.Walk,Enum_PlayerState.Idle, _ => BeIdle());
        
        stateMachine.AddTransition(Enum_PlayerState.Targeting, Enum_PlayerState.Idle, _ => TargetingToIdle());
        stateMachine.AddTransition(Enum_PlayerState.Targeting, Enum_PlayerState.Walk, _ => TargetingToWalk());
        
        stateMachine.AddTransition(Enum_PlayerState.Idle, Enum_PlayerState.Targeting, _ => IdleToTargeting());
        stateMachine.AddTransition(Enum_PlayerState.Walk, Enum_PlayerState.Targeting, _ => WalkToTargeting());

        stateMachine.SetStartState(Enum_PlayerState.Idle);
        stateMachine.Init();
    }
    bool TargetingToWalk()
    {
        return Joystick.Instance.Touching && !playerBrain.attackSystem.IsTargeting;
    }
    bool TargetingToIdle()
    {
        return !playerBrain.attackSystem.IsTargeting && !Joystick.Instance.Touching;
    }
    bool BeIdle()
    {
        return !Joystick.Instance.Touching && !playerBrain.attackSystem.IsTargeting;
    }
    bool BeWalking()
    {
        return Joystick.Instance.Touching && !playerBrain.attackSystem.IsTargeting;
    }
    //--------------------------------------------------------------------------------\\
    bool WalkToTargeting()
    {
        return Joystick.Instance.Touching && playerBrain.attackSystem.IsTargeting;
    }
    bool IdleToTargeting()
    {
        return playerBrain.attackSystem.IsTargeting && !Joystick.Instance.Touching;
    }
}