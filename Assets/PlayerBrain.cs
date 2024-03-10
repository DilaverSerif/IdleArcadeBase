using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    [BoxGroup("Player Data")]
    public PlayerInGameData inGameData;
    [BoxGroup("Player Data")]
    public PlayerDefaultData defaultData;
    
    [BoxGroup("Player Systems"),ReadOnly]
    public PlayerAnimationSystem playerAnimation;
    [BoxGroup("Player Systems"),ReadOnly]
    public PlayerMovementSystem playerMovement;
    [BoxGroup("Player Systems"),ReadOnly]
    public PlayerSoundSystem playerSound;

    void Awake()
    {
        playerAnimation = new PlayerAnimationSystem(this,GetComponentInChildren<Animator>());
        playerMovement = new PlayerMovementSystem(this,GetComponent<CharacterController>());
        playerSound = new PlayerSoundSystem(this);
        inGameData = new PlayerInGameData(defaultData);
    }
}