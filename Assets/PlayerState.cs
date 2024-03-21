using System;
using ComboSystem.Player.ComboSystem.Player;
using UnityHFSM;
namespace ComboSystem.Player
{
    public enum Enum_PlayerState
    {
        None = 0,
        Idle,
        Walk,
        Targeting,
        Run,
    }
    
    namespace ComboSystem.Player
    {
    }
    
    public abstract class PlayerState : State<Enum_PlayerState, PlayerStateEventData>
    {
        protected readonly PlayerBrain PlayerBrain;
        protected readonly float ExitTime;
        protected bool RequestedExit;

        protected readonly Action<State<Enum_PlayerState, PlayerStateEventData>> onEnter;
        protected readonly Action<State<Enum_PlayerState, PlayerStateEventData>> onLogic;
        protected readonly Func<State<Enum_PlayerState, PlayerStateEventData>, bool> canExit;
        protected Action<State<Enum_PlayerState, PlayerStateEventData>> onExit;
        
        public PlayerState(
            PlayerBrain playerBrain,
            bool needsExitTime = false,
            float exitTime = 0,
            Action<State<Enum_PlayerState, PlayerStateEventData>> onEnter = null,
            Action<State<Enum_PlayerState, PlayerStateEventData>> onLogic = null,
            Action<State<Enum_PlayerState, PlayerStateEventData>> onExit = null,
            Func<State<Enum_PlayerState, PlayerStateEventData>, bool> canExit = null
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
}