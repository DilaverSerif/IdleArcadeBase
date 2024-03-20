using System;
using ComboSystem.Player;
using ComboSystem.Player.ComboSystem.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityHFSM;

public class PlayerBrain : MonoBehaviour
{
    [BoxGroup("Player Data")]
    public PlayerInGameData inGameData;
    [BoxGroup("Player Data")]
    public PlayerDefaultData defaultData;

    [BoxGroup("Player Systems")]
    public PlayerAnimationSystem playerAnimation;
    [BoxGroup("Player Systems")]
    public PlayerMovementSystem playerMovement;
    [BoxGroup("Player Systems")]
    public PlayerSoundSystem playerSound;
    
    public StatUser statUser;
    public HealthSystem healthSystem;

    private StateMachine<Enum_PlayerState, PlayerStateEventData> stateMachine;


    void Awake()
    {
        playerAnimation = new PlayerAnimationSystem(this, GetComponentInChildren<Animator>());
        playerMovement = new PlayerMovementSystem(this, GetComponent<CharacterController>());
        playerSound = new PlayerSoundSystem(this);
        inGameData = new PlayerInGameData(this);

        stateMachine = new StateMachine<Enum_PlayerState, PlayerStateEventData>();
    }

    void Start()
    {
        OnSetStates();
    }

    private void OnSetStates()
    {
        var idleState = new PlayerIdleState(this);
        var walkState = new PlayerWalkState(this);

        stateMachine.AddState(Enum_PlayerState.Idle, idleState);
        stateMachine.AddState(Enum_PlayerState.Walk, walkState);

        stateMachine.AddTransitionFromAny(Enum_PlayerState.Idle, _ => !Joystick.Instance.Touching);
        stateMachine.AddTransitionFromAny(Enum_PlayerState.Walk, _ => Joystick.Instance.Touching);

        stateMachine.SetStartState(Enum_PlayerState.Idle);
        stateMachine.Init();
    }

    void FixedUpdate()
    {
        stateMachine.OnLogic();
        playerMovement.Move();  
    }

    void OnDisable()
    {
        inGameData.Dispose();
    }
}