using Sirenix.OdinInspector;
using UnityEngine;

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
    [BoxGroup("Player Systems")]
    public PlayerStateMachine playerStateMachine;
    
    public StatUser statUser;
    public HealthSystem healthSystem;
    public AttackSystem attackSystem;
    
    void Awake()
    {
        attackSystem = GetComponent<PlayerAttackSystem>();
        healthSystem = GetComponent<HealthSystem>();
        
        playerAnimation = new PlayerAnimationSystem(this, GetComponentInChildren<Animator>());
        playerMovement = new PlayerMovementSystem(this, GetComponent<CharacterController>());
        playerSound = new PlayerSoundSystem(this);
        inGameData = new PlayerInGameData(this);
        playerStateMachine = new PlayerStateMachine(this);
    }
    
    void FixedUpdate()
    {
        playerStateMachine.StateMachine.OnLogic();
        playerMovement.Move();  
    }

    void OnDisable()
    {
        inGameData.Dispose();
    }
}