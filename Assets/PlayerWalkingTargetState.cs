using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using UnityHFSM;
public class PlayerWalkingTargetState: PlayerState
{
    public PlayerWalkingTargetState(PlayerBrain playerBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_PlayerState, PlayerStateEventData>> onEnter = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onLogic = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onExit = null, Func<State<Enum_PlayerState, PlayerStateEventData>, bool> canExit = null) : base(playerBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
    }


    public override void OnLogic()
    {
        base.OnLogic();
        PlayerBrain.playerMovement.Rotate(PlayerBrain.attackSystem.GetClosestTarget());
        PlayerBrain.playerMovement.Move();
        PlayerBrain.playerMovement.LocomotionRaise();
    }

}