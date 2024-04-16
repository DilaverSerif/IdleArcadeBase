using System;
using UnityEngine;
[Serializable]
public class PlayerMovementSystem : CharacterSystem<PlayerBrain>
{
    private CharacterController _characterController;
    private float CurrentWalkTime
    {
        get => brain.inGameData.CurrentWalkTime;
        set => brain.inGameData.CurrentWalkTime = value;
    }
    private float CurrentMaxWalkTime
    {
        get => brain.inGameData.CurrentMaxWalkTime;
        set => brain.inGameData.CurrentMaxWalkTime = value;
    }


    private float MaxWalkTime => brain.inGameData.moveSpeedCurve.keys[brain.inGameData.moveSpeedCurve.length - 1].time;
    private float Acceleration => brain.inGameData.acceleration;
    private float MaxSpeed => brain.inGameData.maxSpeed;

    private AnimationCurve MoveSpeedCurve => brain.inGameData.moveSpeedCurve;
    private AnimationCurve RotationTurnSpeedCurve => brain.inGameData.rotationTurnSpeedCurve;
    private PlayerInGameData.RotationType RotationType => brain.inGameData.rotationType;

    private Vector3 TargetPosition
    {
        get => brain.inGameData.targetPosition;
        set => brain.inGameData.targetPosition = value;
    }
    private Quaternion TargetRotation
    {
        get => brain.inGameData.targetRotation;
        set => brain.inGameData.targetRotation = value;
    }


    public PlayerMovementSystem(PlayerBrain brain, CharacterController characterController) : base(brain)
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
        var targetPosition = GetMovePosition() * MoveSpeedCurve.Evaluate(CurrentWalkTime);
        targetPosition.y = -9.81f;
        
        _characterController.Move(targetPosition * Time.deltaTime);

        TargetPosition = targetPosition;
        return targetPosition;
    }

    //<summary>
    //  Hedefe doğru hareket
    //  targetPosition: Hedefin pozisyonu
    //</summary>
    public Vector3 MoveByTarget(Vector3 targetPosition)
    {
        // TargetPosition = Joystick.Instance.Direction3D;
        targetPosition.y = -9.81f;
        var currentPosition = targetPosition * MoveSpeedCurve.Evaluate(CurrentWalkTime);

        _characterController.Move(currentPosition * Time.deltaTime);

        return targetPosition;
    }
    
    //<summary>
    //  Son joystick son durumununa göre hareket ettirmeyi sürdürür
    //</summary>
    
    public Vector3 MoveLastJoystick()
    {
        var targetPosition = Joystick.Instance.LastDirection * MoveSpeedCurve.Evaluate(CurrentWalkTime);
        targetPosition.y = -9.81f;
        
        _characterController.Move(targetPosition * Time.deltaTime);

        TargetPosition = targetPosition;
        return targetPosition;
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
        Debug.Log("Diff Angel:"+GetDifferenceAngle(joystickAngle));
        var currentRotationSpeed = RotationTurnSpeedCurve.Evaluate(GetDifferenceAngle(joystickAngle));
        Debug.Log("Current RotationSpeed:"+currentRotationSpeed);

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
        Debug.Log("Current RotationSpeed:"+currentRotationSpeed);
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
        return Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle));
    }

    public override void OnDrawGizmos()
    {
        if(!debug) return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, TargetPosition);
    }

    public override void OnGUI()
    {
        if(!debug) return;
        
        GUI.Label(new Rect(10, 10, 100, 20), "CurrentWalkTime: " + CurrentWalkTime);
        GUI.Label(new Rect(10, 30, 100, 20), "CurrentMaxWalkTime: " + CurrentMaxWalkTime);
    }
    public float GetCurrentSpeed()
    {
        return _characterController.velocity.magnitude / MaxSpeed;
    }
}