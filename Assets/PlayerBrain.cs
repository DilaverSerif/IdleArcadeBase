using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

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
    public PlayerSoundSystem PlayerSound;
    [BoxGroup("Player Systems")]
    public PlayerStateMachine playerStateMachine;
    
    public StatUser statUser;
    public HealthSystem<PlayerBrain> healthSystem;
    public AttackSystem attackSystem;
    
    void Awake()
    {
        attackSystem = GetComponent<PlayerAttackSystem>();
        healthSystem = GetComponent<HealthSystem<PlayerBrain>>();
        healthSystem.InitializeHealthSystem(this,defaultData.maxHealth);
        
        playerAnimation = new PlayerAnimationSystem(this, GetComponentInChildren<Animator>());
        playerMovement = new PlayerMovementSystem(this, GetComponent<CharacterController>());
        PlayerSound = new PlayerSoundSystem(this);
        inGameData = new PlayerInGameData(this);
        playerStateMachine = new PlayerStateMachine(this);
    }
    
    void FixedUpdate()
    {
        playerStateMachine.StateMachine.OnLogic();
    }
    
    private void Update()
    {
        playerAnimation.OnUpdate();
    }

    void OnDisable()
    {
        inGameData.Dispose();
    }
}