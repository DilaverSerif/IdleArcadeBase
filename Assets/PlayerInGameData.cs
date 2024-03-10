using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class PlayerInGameData
{
    public enum RotationType
    {
        None,
        LookAt,
        RotateTowards,
        Lerp,
        Slerp
    }
    
    [BoxGroup("Movement")]
    public AnimationCurve moveSpeedCurve;
    [BoxGroup("Movement")]
    public float acceleration;
    
    [BoxGroup("Rotation")] 
    public AnimationCurve rotationTurnSpeedCurve;
    [BoxGroup("Rotation")] 
    public RotationType rotationType;

    [NonSerialized,ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float CurrentWalkTime;
    [NonSerialized,ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float CurrentMaxWalkTime;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float maxWalkTime;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public float maxSpeed;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public Vector3 targetPosition;
    [ShowInInspector, ReadOnly, BoxGroup("Debug")]
    public Quaternion targetRotation;
    public PlayerInGameData(PlayerDefaultData defaultData)
    {
        moveSpeedCurve = defaultData.movementCurve;
        acceleration = defaultData.acceleration;
        rotationTurnSpeedCurve = defaultData.rotationTurnSpeedCurve;
        rotationType = defaultData.rotationType;
        
        maxWalkTime = moveSpeedCurve.keys[moveSpeedCurve.length - 1].time;
        maxSpeed = moveSpeedCurve.keys[moveSpeedCurve.length - 1].value;
    }
}
