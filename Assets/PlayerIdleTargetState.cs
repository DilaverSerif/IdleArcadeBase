using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using UnityEngine;
using UnityHFSM;
public class PlayerIdleTargetState: PlayerState
{
    public PlayerIdleTargetState(PlayerBrain playerBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_PlayerState, PlayerStateEventData>> onEnter = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onLogic = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onExit = null, Func<State<Enum_PlayerState, PlayerStateEventData>, bool> canExit = null) : base(playerBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
    }
    
    public override void OnLogic()
    {
        var closestTarget = PlayerBrain.attackSystem.GetClosestTarget();

        if (closestTarget != Vector3.zero)
        {
            PlayerBrain.playerMovement.Rotate(PlayerBrain.attackSystem.GetClosestTarget());
        }
        
        else PlayerBrain.playerMovement.Rotate();
        
        PlayerBrain.playerMovement.MoveLastJoystick();
        PlayerBrain.playerMovement.LocomotionLower();
    }
}