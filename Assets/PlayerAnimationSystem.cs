using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationSystem: PlayerSystem
{
    public Animator defaultAnimator;
    public Vector3 direction;

    private static readonly int Target = Animator.StringToHash("Target");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int SideDir = Animator.StringToHash("Dir");
    public PlayerAnimationSystem(PlayerBrain playerBrain,Animator defaultAnimator) : base(playerBrain)
    {
        this.defaultAnimator = defaultAnimator;
    }

    public override void OnUpdate()
    {
        defaultAnimator.SetBool(Target, playerBrain.attackSystem.IsTargeting);
        var currentSpeed = playerBrain.playerMovement.GetCurrentSpeed();
        
        if (defaultAnimator.GetBool(Target))
        {
            var transformForward = transform.forward;
            var joyStickDirection = Joystick.Instance.GetDirection(true);

            var zAxis = joyStickDirection.z;
            var xAxis = joyStickDirection.x;

            direction = new Vector3(
                (0 - transformForward.x) * zAxis + transformForward.z * xAxis,
                0,
                transformForward.x * xAxis + transformForward.z * zAxis);

            direction *= currentSpeed;
            defaultAnimator.SetFloat(SideDir, direction.x);
            defaultAnimator.SetFloat(Speed, direction.z);
        }
        else
        {
            defaultAnimator.SetFloat(Speed,currentSpeed);
        }
    }
}