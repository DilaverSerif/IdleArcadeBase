using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationSystem: PlayerSystem
{
    public Animator defaultAnimator;
    public PlayerAnimationSystem(PlayerBrain playerBrain,Animator defaultAnimator) : base(playerBrain)
    {
        this.defaultAnimator = defaultAnimator;
    }
}