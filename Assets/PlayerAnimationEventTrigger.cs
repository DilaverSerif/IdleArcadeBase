using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEventTrigger : MonoBehaviour
{
    public PlayerBrain playerBrain;
    
    public void AttackEvent()
    {
        playerBrain.attackSystem.Attack();
    }
}