using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using Sirenix.OdinInspector;
using UnityHFSM;
[Serializable]
public class PlayerStateMachine : BaseStateMachine<Enum_PlayerState, PlayerStateEventData, PlayerBrain>
{
    PlayerIdleState idleState;
    PlayerWalkState walkState;
    PlayerWalkingTargetState walkingTargetState;
    PlayerIdleTargetState idleTargetState;
    
    public PlayerStateMachine(PlayerBrain brain) : base(brain)
    {
    }
    
    protected override void OnCreateStates()
    {
        idleState = new PlayerIdleState(brain);
        walkState = new PlayerWalkState(brain);
        walkingTargetState = new PlayerWalkingTargetState(brain);
        idleTargetState = new PlayerIdleTargetState(brain);
    }
    protected override void OnSetTransitions()
    {
        stateMachine.AddTransition(Enum_PlayerState.Idle, Enum_PlayerState.Walk, _ => BeWalking());
        stateMachine.AddTransition(Enum_PlayerState.Walk, Enum_PlayerState.Idle, _ => BeIdle());

        stateMachine.AddTransition(Enum_PlayerState.WalkTargeting, Enum_PlayerState.Idle, _ => TargetingToIdle());
        stateMachine.AddTransition(Enum_PlayerState.WalkTargeting, Enum_PlayerState.Walk, _ => TargetingToWalk());

        stateMachine.AddTransition(Enum_PlayerState.Idle, Enum_PlayerState.WalkTargeting, _ => IdleToWalkingTargeting());
        stateMachine.AddTransition(Enum_PlayerState.Walk, Enum_PlayerState.WalkTargeting, _ => WalkToWalkingTargeting());

        stateMachine.AddTransition(Enum_PlayerState.IdleTargeting, Enum_PlayerState.WalkTargeting, _ => IdleToWalkingTargetingToWalkingTargeting());
        stateMachine.AddTransition(Enum_PlayerState.WalkTargeting, Enum_PlayerState.IdleTargeting, _ => TargetingToTargetingIdle());

        stateMachine.AddTransition(Enum_PlayerState.IdleTargeting, Enum_PlayerState.Walk, _ => IdleToWalkingTargetingToWalk());
    }
    protected override void OnSetStates()
    {
        stateMachine.AddState(Enum_PlayerState.Idle, idleState);
        stateMachine.AddState(Enum_PlayerState.Walk, walkState);
        stateMachine.AddState(Enum_PlayerState.WalkTargeting, walkingTargetState);
        stateMachine.AddState(Enum_PlayerState.IdleTargeting, idleTargetState);
    }
    public override Enum_PlayerState GetStartingState()
    {
        return Enum_PlayerState.Idle;
    }
    
    bool IdleToWalkingTargetingToWalk()
    {
        return !brain.attackSystem.IsTargeting && Joystick.Instance.Touching;
    }
    bool TargetingToTargetingIdle()
    {
        return brain.attackSystem.IsTargeting && !Joystick.Instance.Touching;
    }
    bool IdleToWalkingTargetingToWalkingTargeting()
    {
        return brain.attackSystem.IsTargeting && Joystick.Instance.Touching;
    }
    bool TargetingToWalk()
    {
        return Joystick.Instance.Touching && !brain.attackSystem.IsTargeting;
    }
    bool TargetingToIdle()
    {
        return !brain.attackSystem.IsTargeting && !Joystick.Instance.Touching;
    }
    bool BeIdle()
    {
        return !Joystick.Instance.Touching && !brain.attackSystem.IsTargeting;
    }
    bool BeWalking()
    {
        return Joystick.Instance.Touching && !brain.attackSystem.IsTargeting;
    }
    //--------------------------------------------------------------------------------\\
    bool WalkToWalkingTargeting()
    {
        return Joystick.Instance.Touching && brain.attackSystem.IsTargeting;
    }
    bool IdleToWalkingTargeting()
    {
        return brain.attackSystem.IsTargeting && !Joystick.Instance.Touching;
    }

}