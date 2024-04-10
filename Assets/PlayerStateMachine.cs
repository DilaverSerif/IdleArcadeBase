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

    PlayerIdleAttackState idleAttackState;
    PlayerWalkAttackState walkAttackState;

    public PlayerStateMachine(PlayerBrain brain) : base(brain)
    {
    }

    protected override void OnCreateStates()
    {
        idleState = new PlayerIdleState(brain);
        walkState = new PlayerWalkState(brain);
        walkingTargetState = new PlayerWalkingTargetState(brain);
        idleTargetState = new PlayerIdleTargetState(brain);

        idleAttackState = new PlayerIdleAttackState(brain);
        walkAttackState = new PlayerWalkAttackState(brain);
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

        stateMachine.AddTransition(Enum_PlayerState.WalkTargeting, Enum_PlayerState.WalkAttacking, _ => WalkTargetingToWalkAttacking());
        stateMachine.AddTransition(Enum_PlayerState.IdleTargeting, Enum_PlayerState.IdleAttacking, _ => IdleTargetingToIdleAttacking());

        stateMachine.AddTransition(Enum_PlayerState.WalkAttacking, Enum_PlayerState.Walk, _ => WalkAttackingToWalk());
        stateMachine.AddTransition(Enum_PlayerState.IdleAttacking, Enum_PlayerState.Idle, _ => IdleAttackingToIdle());

        stateMachine.AddTransition(Enum_PlayerState.WalkAttacking, Enum_PlayerState.WalkTargeting, _ => WalkAttackingToWalkTargeting());
        stateMachine.AddTransition(Enum_PlayerState.IdleAttacking, Enum_PlayerState.IdleTargeting, _ => IdleAttackingToIdleTargeting());

        stateMachine.AddTransition(Enum_PlayerState.WalkAttacking, Enum_PlayerState.IdleAttacking, _ => WalkAttackingToIdleAttacking());
        stateMachine.AddTransition(Enum_PlayerState.IdleAttacking, Enum_PlayerState.WalkAttacking, _ => IdleAttackingToWalkAttacking());
    }
    protected override void OnSetStates()
    {
        stateMachine.AddState(Enum_PlayerState.Idle, idleState);
        stateMachine.AddState(Enum_PlayerState.Walk, walkState);
        stateMachine.AddState(Enum_PlayerState.WalkTargeting, walkingTargetState);
        stateMachine.AddState(Enum_PlayerState.IdleTargeting, idleTargetState);

        stateMachine.AddState(Enum_PlayerState.IdleAttacking, idleAttackState);
        stateMachine.AddState(Enum_PlayerState.WalkAttacking, walkAttackState);
    }
    public override Enum_PlayerState GetStartingState()
    {
        return Enum_PlayerState.Idle;
    }
    bool IdleAttackingToIdleTargeting()
    {
        return !Joystick.Instance.Touching && brain.attackSystem.IsTargeting;
    }
    bool WalkAttackingToWalkTargeting()
    {
        return Joystick.Instance.Touching && brain.attackSystem.IsTargeting;
    }
    bool IdleAttackingToWalkAttacking()
    {
        return Joystick.Instance.Touching && brain.attackSystem.IsTargeting && brain.attackSystem.CanAttack;
    }
    bool WalkAttackingToIdleAttacking()
    {
        return !Joystick.Instance.Touching && brain.attackSystem.IsTargeting && brain.attackSystem.CanAttack;
    }
    bool IdleAttackingToIdle()
    {
        return !Joystick.Instance.Touching && !brain.attackSystem.IsTargeting;
    }
    bool WalkAttackingToWalk()
    {
        return Joystick.Instance.Touching && !brain.attackSystem.IsTargeting;
    }



    bool IdleTargetingToIdleAttacking()
    {
        return brain.attackSystem.IsTargeting && !Joystick.Instance.Touching && brain.attackSystem.CanAttack;
    }
    bool WalkTargetingToWalkAttacking()
    {
        return Joystick.Instance.Touching && brain.attackSystem.IsTargeting && brain.attackSystem.CanAttack;
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