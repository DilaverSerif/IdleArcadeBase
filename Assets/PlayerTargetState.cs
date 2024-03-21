using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using UnityHFSM;
public class PlayerTargetState: PlayerState
{
    public PlayerTargetState(PlayerBrain playerBrain, bool needsExitTime = false, float exitTime = 0, Action<State<Enum_PlayerState, PlayerStateEventData>> onEnter = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onLogic = null, Action<State<Enum_PlayerState, PlayerStateEventData>> onExit = null, Func<State<Enum_PlayerState, PlayerStateEventData>, bool> canExit = null) : base(playerBrain, needsExitTime, exitTime, onEnter, onLogic, onExit, canExit)
    {
    }


    public override void OnLogic()
    {
        base.OnLogic();
        PlayerBrain.playerMovement.Rotate(PlayerBrain.attackSystem.GetClosestTarget());
        PlayerBrain.playerMovement.LocomotionRaise();
    }

}