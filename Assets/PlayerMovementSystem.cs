using System;
using UnityEngine;
[Serializable]
public class PlayerMovementSystem : PlayerSystem
{
    private CharacterController _characterController;
    private float CurrentWalkTime
    {
        get => playerBrain.inGameData.CurrentWalkTime;
        set => playerBrain.inGameData.CurrentWalkTime = value;
    }
    private float CurrentMaxWalkTime
    {
        get => playerBrain.inGameData.CurrentMaxWalkTime;
        set => playerBrain.inGameData.CurrentMaxWalkTime = value;
    }


    private float MaxWalkTime => playerBrain.inGameData.moveSpeedCurve.keys[playerBrain.inGameData.moveSpeedCurve.length - 1].time;
    private float Acceleration => playerBrain.inGameData.acceleration;

    private AnimationCurve MoveSpeedCurve => playerBrain.inGameData.moveSpeedCurve;
    private AnimationCurve RotationTurnSpeedCurve => playerBrain.inGameData.rotationTurnSpeedCurve;
    private PlayerInGameData.RotationType RotationType => playerBrain.inGameData.rotationType;

    private Vector3 TargetPosition
    {
        get => playerBrain.inGameData.targetPosition;
        set => playerBrain.inGameData.targetPosition = value;
    }
    private Quaternion TargetRotation
    {
        get => playerBrain.inGameData.targetRotation;
        set => playerBrain.inGameData.targetRotation = value;
    }


    public PlayerMovementSystem(PlayerBrain playerBrain, CharacterController characterController) : base(playerBrain)
    {
        _characterController = characterController;
    }

    //<summary>
    //  Hareketi durdurur
    //</summary>
    public void StopMoveImmediately(bool pos = true, bool rot = true)
    {
        if (pos)
        {
            CurrentWalkTime = 0f;
            _characterController.Move(Vector3.zero);
        }

        if (rot) transform.rotation = Quaternion.identity;
    }

    //<summary>
    //  Hızı arttır
    //</summary>
    public void LocomotionRaise()
    {
        CurrentMaxWalkTime = MaxWalkTime * Joystick.Instance.Direction.magnitude;

        CurrentWalkTime += Time.deltaTime * Acceleration * Joystick.Instance.Direction.magnitude;
        CurrentWalkTime = CurrentWalkTime > CurrentMaxWalkTime
            ? CurrentWalkTime = CurrentMaxWalkTime
            : CurrentWalkTime;
    }

    // <summary>
    //  Hızı azalt
    // </summary>
    public void LocomotionLower()
    {
        CurrentWalkTime -= Time.deltaTime * Acceleration;
        CurrentWalkTime = CurrentWalkTime < 0f ? CurrentWalkTime = 0f : CurrentWalkTime;
    }

    //<summary>
    //  Joystick yönünde hareket
    //</summary>
    public Vector3 Move()
    {
        TargetPosition = GetMovePosition() * MoveSpeedCurve.Evaluate(CurrentWalkTime);

        TargetPosition.y = -9.81f;
        _characterController.Move(TargetPosition * Time.deltaTime);

        return TargetPosition;
    }

    //<summary>
    //  Hedefe doğru hareket
    //  targetPosition: Hedefin pozisyonu
    //</summary>
    public Vector3 MoveByTarget(Vector3 targetPosition)
    {
        TargetPosition = Joystick.Instance.Direction3D;
        TargetPosition.y = -9.81f;
        var currentPosition = TargetPosition * MoveSpeedCurve.Evaluate(CurrentWalkTime);

        _characterController.Move(currentPosition * Time.deltaTime);

        return TargetPosition;
    }

    //<summary>
    //  Joystick yönünde dönüş
    //</summary>
    public Quaternion Rotate()
    {
        // _rotationTime += Time.deltaTime;
        var jsDirection = GetMovePosition().normalized;
        // Joystick açısını hesapla
        var joystickAngle = Mathf.Atan2(jsDirection.x, jsDirection.z) * Mathf.Rad2Deg;
        // Karakteri joystick açısına döndür
        if (!(Mathf.Abs(jsDirection.x) > 0.1f) && !(Mathf.Abs(jsDirection.z) > 0.1f)) return default;

        TargetRotation = Quaternion.Euler(0f, joystickAngle, 0);
        var currentRotationSpeed = RotationTurnSpeedCurve.Evaluate(GetDifferenceAngle(joystickAngle));

        var rotation = transform.rotation;

        rotation = RotationType switch
        {
            PlayerInGameData.RotationType.Lerp => Quaternion.LerpUnclamped(rotation, TargetRotation,
                currentRotationSpeed * Time.deltaTime),
            PlayerInGameData.RotationType.Slerp => Quaternion.SlerpUnclamped(rotation, TargetRotation,
                currentRotationSpeed * Time.deltaTime),
            PlayerInGameData.RotationType.None => rotation,
            PlayerInGameData.RotationType.LookAt => Quaternion.LookRotation(TargetPosition),
            _ => rotation
        };

        transform.rotation = rotation;
        return rotation;
    }

    //<summary>
    //  Hedefe doğru dönüş
    //  targetPosition: Hedefin pozisyonu
    //</summary>
    public Quaternion Rotate(Vector3 targetPosition)
    {
        var direction = targetPosition - transform.position;
        // Joystick açısını hesapla
        float joystickAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        TargetRotation = Quaternion.Euler(0f, joystickAngle, 0f);
        var currentRotationSpeed = RotationTurnSpeedCurve.Evaluate(GetDifferenceAngle(joystickAngle));

        var rotation = transform.rotation;

        rotation = RotationType switch
        {
            PlayerInGameData.RotationType.Lerp => Quaternion.LerpUnclamped(rotation, TargetRotation,
                currentRotationSpeed * Time.deltaTime),
            PlayerInGameData.RotationType.Slerp => Quaternion.SlerpUnclamped(rotation, TargetRotation,
                currentRotationSpeed * Time.deltaTime),
            PlayerInGameData.RotationType.None => rotation,
            PlayerInGameData.RotationType.LookAt => Quaternion.LookRotation(targetPosition),
            _ => rotation
        };
        transform.rotation = rotation;

        return rotation;
    }

    public Vector3 GetMovePosition()
    {
        return Joystick.Instance.Direction3D == Vector3.zero ? transform.forward : Joystick.Instance.Direction3D;
    }
    public float GetDifferenceAngle(float targetAngle)
    {
        return Quaternion.Angle(transform.rotation, TargetRotation) - targetAngle;
    }
}